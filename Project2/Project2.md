# 1920APJacobsJef
# inleiding
De bedoeling van dit project is om de kaart van belgie in 3D te making aan de hand van een geojson file die de grenspunten bevat. Het omzetten naar 3D wordt gedaan aan de hand van triangulisation.
Aangezien belgië veel grenspunten heeft wordt er gebruikt gemaakt van line simplification dit prosses sneller te laten gaan.

# Voorstudie
Om aan dit project te kunnen beginnen moest ik info opzoeken over: line simplification, triangulisation, geojson files en kaart projecties. 
Er bestaan meerdere algoritmes voor line simplification maar ik gebruik Ramer-Douglas-Peucker algorithm. Voor triangulisation gebruik ik een ear clipping algorithme.
## Ramer-Douglas-Peucker
Het idee van dit algorithme is om de punten die dicht bij de lijn liggen weg te laten waardoor er niet veel veranderd aan de algemene figuur.

#### Werking

Het algorithme maakt een lijn tussen het eerste (P<sub>b</sub>)en laatste punt van de lijn vervolgens wordt er gekeken naar het punt dat het verste van deze lijn ligt we zullen dit punt P noemen. 
Vervolgens zal er worden gekeken naar de punten tussen het P<sub>b</sub> en P als de punten binnen de maximum gezette afstand van de lijn liggen kunnen ze worden weg gelaten.
als dit niet zo is wordt dit punt behouden. Als dit gedaan is het eerste punt een verder daan het vorige eerste punt. dit wordt herhaald tot alle punten zijn afgelopen.

![](https://upload.wikimedia.org/wikipedia/commons/3/30/Douglas-Peucker_animated.gif)
## Ear cliping
Ear cliping is een triangulisation algorithme die door de Ears van de polygon weg te laten.  <br>
Een hoek is een ear van de polygon als de diagonaal van de twee buur hoeken voledig in de figuur ligt.
#### Werking

Eerst wordt er na gegaan welke hoeken een convex zijn. Een ear kan alleen een convexe hoek zijn. Vervolgens worden er gekeken of de diagonaal van de twee buurhoeken
binnen de polygon ligt, dit kan bepaald worden door te kijken of er punten van de polygon binnen de gevormde driehoek ligt als dit niet het geval is dan is het geseleceerde punt een ear. 
als dit het geval is dan mag deze hoek weg en heb je een driehoek met de ear en de twee buur punten.  Dit wordt herhaald tot er nog 3 punten over zijn.

![](Pictures/polygon.png)

# Opbouw code

Eerst worden de punten uit de Geojson file gehaald. Dit zijn echter coordinaten dus moeten omgezet worden door kaart projecties. Als dit gedaan is worden de figuur vereenvoudigd door het
Ramer-Douglas-Peucker algorithme. Dan wordt de figuur omgezet naar driehoeken met het ear cliping algorithme. Als dit gebeurt is wordt de 3D figuur gemaakt, dit wordt gedaan door elke driehoek appart om te zetten naar een 3D figuur. 

# experimentatie

Voor de 3D figuur heb ik voor eigeschap van de hoogte het aantal inwoners per provintie genomen.
Ik ga in de line simplification aanpassen en de licht inval. 

## line simplification


# conclusie


# referentie list
 | nummer          |  bron        | referentie|
|-----------|------------------------|------------:|
| 1 | Wikipedia | [GeoJSON draft version 6](http://wiki.geojson.org/GeoJSON_draft_version_6)   | 
| 2 | Researchgate | [polygon triangulation](https://www.researchgate.net/publication/311245887_Accurate_simple_and_efficient_triangulation_of_a_polygon_by_ear_removal_with_lowest_memory_consumption) | 
| 3 | Wikipedia | [Ramer–Douglas–Peucker algorithm](https://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm) |
| 4 | Wikipedia | [Merkator projection](https://en.wikipedia.org/wiki/Mercator_projection) |
| 5 | Wikipedia | [Vertex(goemetry)](https://en.wikipedia.org/wiki/Vertex_(geometry)) |





 
 
