using Data;
using Logic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DrawCanvas();
            //Test3D();
        }

        private void Test3D()
        {
            MeshGeometry3D cube = new MeshGeometry3D();
            Point3DCollection corners = new Point3DCollection();
            corners.Add(new Point3D(0.5, 0.5, 0.5));
            corners.Add(new Point3D(-0.5, 0.5, 0.5));
            corners.Add(new Point3D(-0.5, -0.01, 0.5));
            corners.Add(new Point3D(0.5, -0.01, 0.5));
            corners.Add(new Point3D(0.5, 0.5, -0.5));
            corners.Add(new Point3D(-0.5, 0.5, -0.5));
            corners.Add(new Point3D(-0.5, -0.01, -0.5));
            corners.Add(new Point3D(0.5, -0.01, -0.5));


            cube.Positions = corners;

            Int32[] indices ={
                //front
                      0,1,2,
                      0,2,3,
                //back
                      4,7,6,
                      4,6,5,
                //Right
                      4,0,3,
                      4,3,7,
                //Left
                      1,5,6,
                      1,6,2,
                //Top
                      1,0,4,
                      1,4,5,
                //Bottom
                      2,6,7,
                      2,7,3
                };
            Int32Collection Triangles =
                         new Int32Collection();
            foreach (Int32 index in indices)
            {
                Triangles.Add(index);
            }

            cube.TriangleIndices = Triangles;

            GeometryModel3D Cube1 = new GeometryModel3D();
            MeshGeometry3D cubeMesh = cube;
            Cube1.Geometry = cubeMesh;

            Color c = Colors.Blue;
            c.A = 170;

            Cube1.Material = new DiffuseMaterial(
            new SolidColorBrush(c));

            DirectionalLight DirLight1 =
                        new DirectionalLight();
            DirLight1.Color = Colors.White;
            DirLight1.Direction = new Vector3D(-1, -15, -10);

            DirectionalLight DirLight2 =
                        new DirectionalLight();
            DirLight1.Color = Colors.White;
            DirLight1.Direction = new Vector3D(-10, -15, -1);

            PerspectiveCamera Camera1 =
                      new PerspectiveCamera();

            Camera1.FarPlaneDistance = 20;
            Camera1.NearPlaneDistance = 1;

            Camera1.FieldOfView = 45;

            Camera1.Position = new Point3D(10, 8, 2);

            Camera1.LookDirection =
                   new Vector3D(-10, -8, -2);

            Camera1.UpDirection = new Vector3D(0, 1, 0);

            Model3DGroup modelGroup = new Model3DGroup();
            modelGroup.Children.Add(Cube1);
            modelGroup.Children.Add(DirLight1);
            modelGroup.Children.Add(DirLight2);

            ModelVisual3D modelsVisual =
                      new ModelVisual3D();
            modelsVisual.Content = modelGroup;

            Viewport3D myViewport = new Viewport3D();
            myViewport.Camera = Camera1;
            myViewport.Children.Add(modelsVisual);

            this.canvas.Children.Add(myViewport);

            myViewport.Height = 1000;
            myViewport.Width = 1000;
            Canvas.SetTop(myViewport, 0);
            Canvas.SetLeft(myViewport, 0);


            //this.Content = myViewport;

        }
        private void DrawCanvas()
        {
            ReadGeo rg = new ReadGeo();
            var prov = rg.ReadJson();
            foreach(var province in prov){
                foreach (var polygon in province.Polygons)
                {
                    FindTopLeft(polygon);
                }
            }

            foreach (var province in prov)
            {
                foreach (var polygon in province.Polygons)
                {
                    canvas.Children.Add(CreatePolygon(polygon,ProvinceColor(province.Name)));
                }
            }

            


        }

        private SolidColorBrush ProvinceColor(string provinceName)
        {
            switch (provinceName)
            {
                case "Antwerp":
                    return Brushes.Red;
                case "East Flanders":
                    return Brushes.Blue;
                case "Flemish Brabant":
                    return Brushes.Yellow;
                case "Hainaut":
                    return Brushes.Azure;
                case "Liège":
                    return Brushes.Aquamarine;
                case "Limburg":
                    return Brushes.Gray;
                case "Luxembourg":
                    return Brushes.CornflowerBlue;
                case "Namur":
                    return Brushes.Cyan;
                case "Walloon Brabant":
                    return Brushes.ForestGreen;
                case "West Flanders":
                    return Brushes.GreenYellow;
                case "Brussels-Capital":
                    return Brushes.Aquamarine;
                default:
                    return Brushes.LightBlue;
            }
        }
        // Latidude > (y)
        // Longitude < (x)
        Point topLeft = new Point(int.MaxValue, int.MinValue);

        public object MeshGeomerty3D { get; private set; }

        private void FindTopLeft(GeoJSON.Net.Geometry.Polygon pol)
        {
            
            foreach (var lineString in pol.Coordinates)
            {
                for (int i = 0; i < lineString.Coordinates.Count; i++)
                {
                    Mercator mercator = new Mercator(canvas.Width, canvas.Height);
                    var temp = mercator.ToPoint(lineString.Coordinates[i].Longitude, lineString.Coordinates[i].Latitude);

                    if ((topLeft.X > temp.x) && (topLeft.Y < temp.y))
                    {
                        topLeft = new Point(temp.x, temp.y);
                    }
                }
            }
        }
        private PointCollection PolygonToPointColl(GeoJSON.Net.Geometry.Polygon pol)
        {
            PointCollection points = new PointCollection();

            //https://gist.github.com/nagasudhirpulla/9b5a192ccaca3c5992e5d4af0d1e6dc4
            int teller = 0;
            foreach (var lineString in pol.Coordinates)
            {
                if (teller == 1) break;
                teller++;
                for (int i = 0; i < lineString.Coordinates.Count; i++)
                {
                    Mercator mercator = new Mercator(canvas.Width, canvas.Height);
                    var temp = mercator.ToPoint(lineString.Coordinates[i].Longitude, lineString.Coordinates[i].Latitude);

                    points.Add(new Point((temp.x + 100) - topLeft.X, (temp.y + 200) - topLeft.Y));
                }
            }


            
            return points;
        }

        private Polygon CreatePolygon(GeoJSON.Net.Geometry.Polygon polygon,SolidColorBrush color)
        {
            Polygon p = new Polygon();
            p.Stroke = Brushes.Black;
            p.Fill = color;
            p.StrokeThickness = 1;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Center;
            ReadGeo rg = new ReadGeo();
            p.Points = PolygonToPointColl(polygon);

           
            return p;
        }

        private List<Point[]> PolygonTriangulation(Polygon polygon)
        {
            List<Point[]> triangles = new List<Point[]>();

            return triangles;
        }
    }
}
