# 1920APJacobsJef
# inleiding
De bedoeling van dit project is om de kaart van belgie in 3D te making aan de hand van een geojson file die de grenspunten bevat. Het omzetten naar 3D wordt gedaan aan de hand van triangulisation.
Aangezien belgië veel grenspunten heeft wordt er gebruikt gemaakt van line simplification dit prosses sneller te laten gaan.

# Voorstudie
Om aan dit project te kunnen beginnen moest ik info opzoeken over: line simplification, triangulisation, geojson files. 
Er bestaan meerdere algoritmes voor line simplification maar ik gebruik Ramer-Douglas-Peucker algorithm. Voor triangulisation gebruik ik een ear clipping algorithme.
## Ramer-Douglas-Peucker
Het idee van dit algorithme is om de punten die dicht bij de lijn liggen weg te laten waardoor er niet veel veranderd aan de algemene figuur.

#### Werking

Het algorithme maakt een lijn tussen het eerste (P<sub>b</sub>)en laatste punt van de lijn vervolgens wordt er gekeken naar het punt dat het verste van deze lijn ligt we zullen dit punt P noemen. 
Vervolgens zal er worden gekeken naar de punten tussen het P<sub>b</sub> en P als de punten binnen de maximum gezette afstand van de lijn liggen kunnen ze worden weg gelaten.
als dit niet zo is wordt dit punt behouden. Als dit gedaan is het eerste punt een verder daan het vorige eerste punt. dit wordt herhaald tot alle punten zijn afgelopen.

[image](https://upload.wikimedia.org/wikipedia/commons/3/30/Douglas-Peucker_animated.gif)
## K-means clustering

#### Werking

#### voordelen

#### nadelen

## Dithering 

# Opbouw code

### 1. winform

### 2. De algoritmes

#### Code median cut

##### Convert


#### Code K-means

##### Convert



#### CreatePaletteMap

#####color palette 16-64

#####color palette 128-256

#### Code convertor

# experimentatie


# conclusie


# referentie list
 
 
