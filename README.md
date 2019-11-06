# 1920APJacobsJef
# inleiding
De bedoeling van dit project is om de colors van een afbeelding te verminderen (Color depth reduction) aan de hand van color quantization.
Er zijn meerdere color quantization algoritmes. Voor dit project maak ik gebruik van 2 algoritmes: median cut en k-means.
Er is ook een mogelijkheid om dithering toe te passen op de gequantizationed afbeelding.
 
# color quantization
Color quantization is een prosses dat het aantal kleuren in een afbeelding verminderd.
Het doel is om een color palette te kiezen zodat je met minder kleuren (max 256 in dit geval) de afbeelding zo goed mogenlijk hermaakt.
Hiervoor worden algoritmes gebruikt. 

# Median cut
Het median cut algoritme is uitgevonden door Paul Heckbert in 1979. 
En is een van de meest populaire algoritmes.
#### Werking
Eerst bepaal je het kleur met de grootste range. <br>

Voorbeeld

| R         |    G     |    B    |
|-----------|----------|--------:|
| 53 - 171  | 20 - 230 | 0 - 255 |

In dit geval heet blauw de grootste range. <br> 

Nu wat we weten welk kleur de grootste range heeft kunnen we onze kleuren gaan sorteren op dat kleur in dit geval blauw. <br>

Als dit gebeurd is gaan we de kleuren splitsen op de mediaan. 
Dus alle kleuren boven de mediaan gaan appart in een 'bucket' en alle kleuren onder de mediaan gaan appart in een 'bucket'. 

Als dit gebeurt is dan hebben we 2 buckets. Maar aangezien we meer kleuren nodig hebben moeten we dit nog een paar keer doen.<br>
We kijken naar de 2 buckets en we vergelijken welke bucket de grootste color range heeft.

|           | R         |    G     |    B    |
|-----------|-----------|----------|--------:|
|Bucket 1    | 53 - 171  | 20 - 230 | 0 - 135 |
|Bucket 2    | 65 - 171  | 60 - 220 | 0 - 255 |

Hier in dit geval heeft het kleur blauw de grootste color range in bucket 2. <br>
Dus splitsen we bucket 2 op de mediaan met als gevolg hebben we 3 buckets. Als we dit blijven doen tot we 256 buckets hebben dan hebben we bijna een goed color palette.
in de 256 buckets zitten meerdere kleuren dus moeten we het gemiddelde van deze kleuren nemen. 

Met als gevolg hebben we een kleur palette gemaakt van 256 kleuren. 

#### Voordelen
+ Het maken van een color palette is redelijk snel
+ Consequent resultaat
+ redelijk goed resultaat

# K-means
Het k-means clustering algoritme werkt door alle kleuren in clusters te verdelen en daaruit dan een color palette maken.
#### Werking
Eerst neem je 256 (of hoeveel kleuren je in je color palette wilt) kleuren. Die gaan onze clusters zijn. <br>
Vervolgens bekijk je de andere kleuren en bepaal je bij welke cluster deze het dichts is plaats deze dan in die cluster. <br>
eens dat alle kleuren in hun clusters zitten wordt het gemiddelde van die cluster berekend. 

dan ga je nog is alle kleuren na en plaats  ze in de dichtste cluster, doe dit tot de clusters niet meer veranderen.
 
Aangezien de initieele clusters random zijn hebben we niet altijd het beste resultaat daarom kunnen we deze stappen herhalen tot we een goed genoeg resultaat krijgen.

![Sample Video](https://youtu.be/4b5d3muPQmA)
# Opbouw code

# experimentatie

# conclusie

