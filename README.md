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

#### voordelen
+ Het maken van een color palette is redelijk snel
+ Consequent resultaat
#### nadelen
- Resultaat niet ideaal
# K-means
Het k-means clustering algoritme werkt door alle kleuren in clusters te verdelen en daaruit dan een color palette maken.
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
#### Median cut
Deze classe heeft 2 public methode [Convert](README.md#####Convert) en [CreatePaletteMap](README.md#####CreatePaletteMap).
##### Convert
Deze bestaat uit 2 delen create palette en omzetten van de bitmap pixels. <br>
eerst wordt er een color palette gemaakt aan de hand van het median cut algoritme dat werdt bescheven.

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
##### CreatePaletteMap

# experimentatie

# conclusie

