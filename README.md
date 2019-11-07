# 1920APJacobsJef
# inleiding
De bedoeling van dit project is om de colors van een afbeelding te verminderen (Color depth reduction) aan de hand van color quantization.
Er zijn meerdere color quantization algoritmes. Voor dit project maak ik gebruik van 2 algoritmes: median cut en k-means.
Er is ook een mogelijkheid om dithering toe te passen op de gequantizationed afbeelding.
 
# Voorstudie
Om aan dit project te kunnen beginnen heb ik info gezocht over: color quantization, median cut, k means clustering en Floyd–Steinberg dithering.
## color quantization
Color quantization is een prosses dat het aantal kleuren in een afbeelding verminderd.
Het doel is om een color palette te kiezen zodat je met minder kleuren (max 256 in dit geval) de afbeelding zo goed mogenlijk hermaakt.
Hiervoor worden algoritmes gebruikt. <sup>[[1]](README.md#referentielist)</sup>

## Median cut
Het median cut algoritme is uitgevonden door Paul Heckbert in 1979. 
En is een van de meest populaire algoritmes. En werkt door de colors te splitsen door de median vandaar de naam "median cut".
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

#### voordelen
+ Het maken van een color palette is redelijk snel
+ Consequent resultaat
#### nadelen
- Resultaat niet ideaal
## K-means clustering
Het k-means clustering algoritme werkt door alle kleuren in clusters te verdelen en dan van die clusters de means (gemiddelde) te berekenen.
#### Werking
Eerst neem je 256 (of hoeveel kleuren je in je color palette wilt) kleuren. Die gaan onze clusters zijn. <br>
Vervolgens bekijk je de andere kleuren en bepaal je bij welke cluster deze het dichts is plaats deze dan in die cluster. <br>
eens dat alle kleuren in hun clusters zitten wordt het gemiddelde van die cluster berekend. 

dan ga je nog is alle kleuren na en plaats  ze in de dichtste cluster, doe dit tot de clusters niet meer veranderen.
 
Aangezien de initieele clusters random zijn hebben we niet altijd het beste resultaat daarom kunnen we deze stappen herhalen tot we een goed genoeg resultaat krijgen.

#### voordelen
+ Goed resultaat
+ Hoe meer run hoe beter het resultaat
#### nadelen
- resultaat is niet Consequent
- meer runs zorgt voor een langere tijd
## Dithering 
Dithering is ruis die wordt toegevoegd aan een afbeelding om color banding te vermideren. Er zijn hier meerdere algortimes.
Deze applicatie maakt gebruik van Floyd–Steinberg dithering. 

<br>* is de huidige pixel. De pixels die al veranderd zijn worden niet meer aangepast. dan wordt de quantization error van de huidige pixel naar de omligende pixels verspreid.
<br>Voorbeeld:
voor pixel[x + 1] (de pixel rechts van * )  <br>

pixel[x + 1] = pixel[x + 1] + error_huidigePixel * 7 / 16

![image](https://wikimedia.org/api/rest_v1/media/math/render/svg/ad305726a5720c59f10c8beb3057c78d43f1fed0)
# Opbouw code
De code is opgebouwd uit 3 hoofd delen.
1. De winform
2. De algoritmes
3. Convertie pixels
### 1. winform
De winform is de userinterface dit gaat dus de classes oproepen waneer nodig. <br>
de belangrijke delen zijn het inladen van de foto geselecteerd door de gebruiker en het aanpassen van de foto naar gelang de instellingen die geselecteerd zijn.

Deze 2 lijnen code lezen het geselecteerd bestand in en zetten het om naar en bitmap. met een bitmap kan je makelijk pixels uitlezen en wegschrijven. 
De bitmap wordt ook ge sized naar de dimeties van de picture box
```
 Image img = Image.FromFile(file);
 image = new Bitmap(img, new Size(pictureBox1.Width, pictureBox1.Height));
```

Afhankelijk van het algoritme wordt een methode opgeroepen. Deze geeft 2 waarden terug een bitmap en een double. <br>
De bitmap is de aangepaste foto en de double de gemiddelde kleur afstand.

```
result = c.convert(colorSize, checkBox1.Checked , progressBar2, label4);
```
Om de color palette te visualiseren wordt er een methode opgeroepen die dan een bitmap returned.
Er is ook een optie om een pagina mee te geven omdat 256 kleuren weergeven op een scherm niet altijd duidelijk is toon ik 32 en een optie om van pagina te veranderen.
```
c.CreatePalletMap(pictureBox3.Width, pictureBox3.Height, ((int.Parse(this.label5.Text))));
```
### 2. De algoritmes
Deze aplicatie maakt gebruik van 2 algoritme ( [median cut](README.md#Median cut) en [k-means](README.md#k-means)). 
#### Code median cut
Deze classe heeft 2 public methode Convert en [CreatePaletteMap](README.md#CreatePaletteMap).
##### Convert
Deze bestaat uit 2 delen create palette en omzetten van de bitmap pixels. <br>
eerst wordt er een color palette gemaakt aan de hand van het [median cut](README.md#Median cut) algoritme dat werdt bescheven.

Eerst wordt de bitmap ingelezen en de pixels worden in een List geplaatst. vervolgens worden ze georderd op het kleur met de greatest range.
```
List<Color> pixelList = GetPixelList(bitmap);
pixelList = orderByGreatestRange(pixelList);
```

Als buckets gebruik ik eeb list van een list van kleuren. en ik vul de eerste met de geordende pixel list.
```
List<List<Color>> buckets = new List<List<Color>>();
buckets.Add(pixelList);
```
Vervolgens split ik de bucket met de greatest range en plaats de 2 nieuwe buckets in de buckets list. Uiteraard wordt de parrent verwijderd<br>
Dit wordt herhaald tot de palletSize wordt berijkt.
```
do
{
    List<Color> colorGR = (findGreatestRange(buckets));
    var itemsCut = Cut(colorGR);
    buckets.Add(itemsCut.Item1);
    buckets.Add(itemsCut.Item2);
    buckets.Remove(colorGR);
} while (buckets.Count < PalletSize);
```

Dan wordt van elke bucket appart het gemiddelde bepaald en toegevoegd aan het color palette.
```
 pallet.Add(BerekenGemiddelde(buckets[i]));
```

#### Code K-means
De k means class heeft 2 methode Convert en [CreatePaletteMap](README.md#CreatePaletteMap).
##### Convert
de convert methode heeft 2 delen, deel 1 is het aanmaken van een color palette en deel 2 is de afbeelding aanpassen aan de hand van het gegeven color palette.

Eerst worden er random clusters gemaakt. Het aantal clusters hangt af van de palette grote. <br>
vervolgens worden alle kleuren in de dichtsbijzijnde cluster geplaatst bepaald door de afstand tussen de cluster.  En dan in een list geplaatst
```
List<Color> ks = getKs(colors, palletSize);
List<Color> clusterMeans = CreateClusters(ks, colors,p);
``` 
Dit wordt herhaald tot er geen verandering meer ik de clusters is.
```
do
{
    prevClusterMeans = clusterMeans;
    clusterMeans = CreateClusters(prevClusterMeans, colors,p);
} while (clusterMeans.Equals(prevClusterMeans));
```
Dit kan x keer herhaald worden om dan de beste te returnen.


#### CreatePaletteMap
In deze methode wordt een color palette visueel gemaakt. (Omzetting naar bitmap).

Aangezien 256 kleuren op 1 scherm niet duidelijk genoeg was heb ik vanaf 128 kleuren een splitsing gemaakt per 32 kleuren met een mogelijkheid om van pagina te veranderen. 

#####color palette 16-64
In de methode SetPixels worden de pixels geset. xTimes en yTimes zijn hoeveel keer een kleur herhaald wordt over de x en y as.
```
SetPixels(b, x, y, xTimes, y / pallet.Count, pallet);
```
#####color palette 128-256
Bij 128 en 256 colors wordt het in paginas opgedeeld dus afhankelijk van de pagina worden 32 kleuren genomen en die worden dan gevisualiseerd.
```
for (int i = (amountPerPage * (page-1)); i <= (amountPerPage * page)-1; i++)
{
    colorsPerPage.Add(pallet[i]); 
}
```

#### Code convertor
Hier wordt er gekeken welke kleur van de color palette het dichts bij het origineel licht en dan wordt het origineel veranderd naar de dichtsbijzijnde kleur.


```
double kleurAfstand = Math.Sqrt(Math.Pow(palette[i].R - c.R, 2) + Math.Pow(palette[i].G - c.G, 2) + Math.Pow(palette[i].B - c.B, 2));

if (kleinsteKleurAfstand > kleurAfstand)
{
    colorIndex = i;
    kleinsteKleurAfstand = kleurAfstand;
}
```
# experimentatie



|           | Origineel         |   256 zonder dithering   |    256 met dithering     |
|-----------|-------------------|--------------------------|--------------------------:|
|Median cut   |  ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/TestPicture2.jpg) | ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/medianCut256.jpg) | ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/medianCut256Dithering.jpg) |
|resultaat  |  | 10.2 gemiddelde kleur afstand | 10.4 gemiddelde kleur afstand |


|           | Origineel         |   256 zonder dithering   |    256 met dithering     |
|-----------|-------------------|--------------------------|--------------------------:|
|kmeans    |  ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/TestPicture2.jpg) | ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/Kmeans256.jpg) | ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/Kmeans256Dithering.jpg) |
|resultaat  |  | 8.6 gemiddelde kleur afstand | 9.4 gemiddelde kleur afstand |

|           | Origineel         |   256 5x   |    256 10x     |
|-----------|-------------------|--------------------------|--------------------------:|
|kmeans     |  ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/TestPicture2.jpg) | ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/Kmeans256x5.jpg) | ![](https://git.ikdoeict.be/jef.jacobs/1920apjacobsjef/raw/master/pictures/Kmeans256x10.jpg) |
|resultaat  |  | 8.7 gemiddelde kleur afstand | 8.6 gemiddelde kleur afstand |








# conclusie
In de median cut tabel kan je zien dat er redelijk veel color banding is. door het gebruik van dithering wordt dit wel beter. 
Maar de gemiddelde kleur afstand wordt wel een beetje minder bij het gebruik van dithering.

Bij k-means is dit wat minder color banding in het algemeen. Maar nog steeds wordt dit beter bij dithering.

het verschil tussen kmeans en median cut is wel merkbaar als je naar de bergen kijkt is er een minder resultaat bij median cut. 
Bijde algoritmen geven een mooi resultaat bij de bloemen. De zon zorgt voor veel color banding bij bijde algoritme.

Het k-means algoritme heeft iedere keer een ander resultaat door dat de clusters random worden gekozen. 
Met als gevolg is het aangeraden om het algoritme meerdere keren te runnen. 
bij de 5x run krijgen we een minder resultaat dan bij de 1x run dit kan zijn omdat we geluk hebben gehad met de 1x run of ongeluk met de 5x.
Bij de 10x run krijgen we een verbetering rond de zon maar een vermindering bij de bergen. de lucht is ook beter bij de 10x.
De bloemen zijn overal ongeveer gelijk. 

# referentie list
 
 | Type          |  Reference        | 
|-----------|-------------------------------:|
|  Color quantization | https://en.wikipedia.org/wiki/Color_quantization   | 
|  Median cut wiki  | https://en.wikipedia.org/wiki/Median_cut  | 
| betere uitleg median cut | http://joelcarlson.github.io/2016/01/15/median-cut/ |
| color quantization  | https://www.codeproject.com/Articles/66341/A-Simple-Yet-Quite-Powerful-Palette-Quantizer-in-C|
|k means clustering| https://dionesiusap.github.io/articles/20180528-color-quantization-using-k-means.html|
|simpele goede uitleg k means | https://nl.go-travels.com/88048-k-means-clustering-1019648-4071969|
