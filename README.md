# Programmation fonctionnelle TP
## Rapide présentation du projet
Le projet est une petite API développée en ASP.Net 6.0 via Visual Studio.
Elle permet la gestion simple de client et de facture (création/récupération).

L'architecture a été simplifiée mais il est tout à fait possible de placer chaque couche dans un projet différent (Presentation, Application, Domain et Infrastructure).

Plusieurs éléments ont été mis en place :
- Du DDD simple via l'utilisation d'un domaine contenant nos objets métier épurés,
- Du CQRS via l'outi MediatR,
- Des tests unitaires et d'intégrations. Ces derniers s'appuient sur l'utilisation de Fixture et d'une base de données mémoire.

Je vais présenter chacunes des notions vues en cours une par une avec l'appui d'exemple. Ces exemples sont non-exhaustifs et d'autres exemples pourront être trouvés dans le code avec des cas d'utilisation différents. 

## Application des notions vues en cours
### **Expression lambda**
L'environnement .Net se base sur le package LinQ afin d'effectuer des requêtes sur des listes (que ce soit en base de données ou sur des listes chargées en mémoire).
La syntaxe LinQ s'appuie justement sur des lambdas afin de filter/sélectionner des données ou des propriétés.

### Exemple dans le code : 
``
var client = await _context.Clients.Include(c => c.Factures).FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);
`` \
On récupère dans le code ci-dessus un client dont l'identifiant correspond à celui passé en paramètre.
LinQ est utilisé dans nos différents

### **Fonctions pures**
Via l'application du DDD, on obtient d'office un domaine complètement pur. En effet, ce dernier n'a aucune référence vers l'extérieur et ne comprend que du code métier pur. 
Plus globalement, les classes de type "Helper" souvent statiques représentent de bons exemples de fonctions pures. 

### Exemple dans le code :
```
public static StringContent ToStringContent(object rawObject)
{
    return new StringContent(
        JsonConvert.SerializeObject(
            rawObject,
            Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
        Encoding.UTF8, "application/json");
}
```
On utilise cette fonction dans les tests d'intégration afin de transformer un objet en Json pour qu'il puisse être envoyé dans une requête HTTP.
D'autres exemples peuvent être trouvés dans la partie "Domain" du code.

### **Immutabilité**
L'implémentation du CQRS via MediatR permet d'assurer l'immutabilité du code dans les requêtes d'écriture via l'utilisation de fonction pure.
Dans le code, cette implémentation est représentée par la création d'un objet requête suffixé par "Query" ainsi qu'un handler correspondant qui traitera cette requête.

### Exemple dans le code :
> CF : GetAllClientsPaginatedQuery.cs , GetAllClientsPaginatedHandler.cs

On peut considérer que ce code est pur quant à la base de données, car on s'assure qu'aucun élément ne sera modifié durant le traitement de la requête.
Par exemple, l'utilisation de la fonction "AsNoTracking" (GetAllClientsPaginatedHandler ligne 20) nous permet de supprimer le tracking géré automatiquement par EntityFramework. 
Si on modifie l'objet récupéré, il est nécessaire de mettre à jour expressémment cet objet dans la base de données grace à la fonction "Context.Update(entity);".

Outre l'implémentation du CQRS dans le code, les accès aux propriétés des objets du code sont restreints au maximum. Par exemple, les propriétés injectées dans nos services sont marquées comme Readonly. Les setters non-nécessaires sont supprimés et ceux nécessaires sont passés en privé et sont modifiables seulement via des fonctions présentes dans l'objet concerné.

### **Idempotence**
Plusieurs fonctions idempotentes ont été implémentées dans le code.
Par exemple, la création et la modification d'un client sont gérés par le même endpoint.
> CF : ClientController.cs , CreateOrUpdateClientDto , CreateOrUpdateClientCommand.cs , CreateOrUpdateClientHandler.cs

Si un identifiant est fourni dans le DTO d'entrée, le client concerné est modifié. Sinon un nouveau client est crée. Dans les deux cas on retourne l'identifiant du client.
Dans le cas de la modification, si on ne trouve pas le client en base, on soulève une exception car cela représente une erreur métier; on souhaite modifier une entité inexistante.

Certaines méthodes des entités du Domaine sont également idempotentes. On peut les éxécuter autant de fois que l'on veut, le resultat sera le même.


## Implémentation des tests unitaires et d'intégration
En séparant les domaines purs et impurs, on peut facilement rédiger un grand nombre de tests unitaires facilement. En effet, peu importe le contexte, le résultat renvoyé par une fonction pure restera le même.
C'est pour cela que des tests unitaires sur des classes Helper ou des entités du domaine sont concis et rapides.

Concernant les tests d'intégration, étant donné que l'on doit gérer des domaines purs et impurs, la rédaction est plus complexe. 
On se base sur l'utilisation de Fixture afin de mettre à disposition à notre classe de test d'intégration un client Http afin de faire nos requêtes sur une pseudo API. Cette dernière utilise une base de données mémoire.
En assemblant tous ces éléments, chaque classe de test a à disposition une base de données que l'on peut préremplir pour effectuer nos tests.
