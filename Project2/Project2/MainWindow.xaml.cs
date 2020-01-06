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
    /// Add -> experimentation voor dockters per m²
    /// Add line vereenvoudiging slider 
    /// Add material 
    public partial class MainWindow : Window
    {
        Viewport3D viewport = new Viewport3D();
        public MainWindow()
        {
            InitializeComponent();

            DrawCanvas();
        }

        private PointCollection LineSimp(PointCollection points)
        {
            LineSimplification ls = new LineSimplification();
            List<Globals.Point> pointListOut = ls.RamerDouglasPeucker(ConvertPointCollToPt(points), 2.5);

            PointCollection pointsOut = new PointCollection();
            foreach (Globals.Point point in pointListOut)
            {
                pointsOut.Add(new Point(point.X, point.Y));
            }

            return pointsOut;
        }


        

    private void DrawTriangles(List<Point[]> triangles)
        {
            for (int i = 0; i < triangles.Count; i++)
            {
                PointCollection points1 = new PointCollection(triangles[i]);
                Polygon polygon = new Polygon();

                polygon.Stroke = Brushes.Black;
                polygon.Fill = Brushes.Transparent;
                polygon.StrokeThickness = 1;
                polygon.HorizontalAlignment = HorizontalAlignment.Left;
                polygon.VerticalAlignment = VerticalAlignment.Center;

                polygon.Points = points1;


                canvas.Children.Add(polygon);
            }
        }
        //https://www.i-programmer.info/projects/38-windows/273-easy-3d.html?start=3
        private MeshGeometry3D TriangleD3(Point p1, Point p2, Point p3, double heigth)
        {
            MeshGeometry3D traiangle = new MeshGeometry3D();
            Point3DCollection corners = new Point3DCollection();

            corners.Add(new Point3D(p1.X, heigth, p1.Y));
            corners.Add(new Point3D(p2.X, heigth, p2.Y));
            corners.Add(new Point3D(p3.X, heigth, p3.Y));

            corners.Add(new Point3D(p1.X, 0, p1.Y));
            corners.Add(new Point3D(p2.X, 0, p2.Y));
            corners.Add(new Point3D(p3.X, 0, p3.Y));



            traiangle.Positions = corners;

            Int32[] indices ={
                //front
                      0,1,2,
                      2,1,0,
                //back
                      5,4,3,
                      3,4,5,
                //Right
                      1,5,2,
                      2,5,1,
                //Left
                      3,5,2,
                      2,5,3,
                //Top
                      0,1,3,
                      3,1,0,

                //Bottom
                      3,4,1,
                      1,4,3,
                };
            Int32Collection triangleIndices =
                         new Int32Collection();
            foreach (Int32 index in indices)
            {
                triangleIndices.Add(index);
            }

            traiangle.TriangleIndices = triangleIndices;
            return traiangle;
        }
        
        private void ShapeTriangle3D(Point[] triangle, double height, SolidColorBrush color)
        {
            GeometryModel3D triangle3D = new GeometryModel3D();

            MeshGeometry3D meshTriangle = TriangleD3(triangle[0], triangle[1], triangle[2], height);
            Vector3D norm = new Vector3D(0, 0, 1);
            meshTriangle.Normals.Add(norm);
            meshTriangle.Normals.Add(norm);
            meshTriangle.Normals.Add(norm);

            triangle3D.Geometry = meshTriangle;
            
            Color c = color.Color;
            c.A = 245;

            color = new SolidColorBrush(c);
            triangle3D.Material = new DiffuseMaterial(color);
            triangle3D.Transform = new Transform3DGroup();

            ModelVisual3D visual3D = new ModelVisual3D();
            visual3D.Content = triangle3D;
            viewport.Children.Add(visual3D);
            

            Model3DGroup model3D = new Model3DGroup();
            model3D.Children.Add(triangle3D);

            ModelVisual3D modelVisual3D = new ModelVisual3D();

            viewport.Children.Add(modelVisual3D);

        }

        private void Draw3D(List<Point[]> triangles,double height,SolidColorBrush color)
        {
            

            for (int i = 0; i < triangles.Count; i++)
            {
                ShapeTriangle3D(triangles[i], height,color);
            }

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

            ReadEig re = new ReadEig();
            Dictionary<string,double> eig = Rescale(re.ReadEigenSchappen());
            foreach (var province in prov)
            {
                foreach (var polygon in province.Polygons)
                {
                    Polygon polygon2;
                    if (province.Name == "West Flanders")
                    {
                        polygon2 = CreatePolygon(polygon, ProvinceColor(province.Name), true);
                    }
                    else
                    {
                        polygon2 = CreatePolygon(polygon, ProvinceColor(province.Name));
                    }

                    // ------ 2D --------

                    //canvas.Children.Add(polygon2);
                    //DrawTriangles(PolygonTriangulation(polygon2));

                    // ------ 3D --------

                    Draw3D(PolygonTriangulation(polygon2), eig[province.Name] , ProvinceColor(province.Name));

                }
            }

            PerspectiveCamera Camera1 = new PerspectiveCamera();
            Camera1.FarPlaneDistance = 10000;
            Camera1.NearPlaneDistance = 200;
            Camera1.FieldOfView = 65;
            Camera1.Position = new Point3D(380, 700, 540);
            Camera1.LookDirection = new Vector3D(0, -10, -4);
            Camera1.UpDirection = new Vector3D(0, 1, 0);

            viewport.Camera = Camera1;

            DirectionalLight myDirLight = new DirectionalLight();

            myDirLight.Color = Colors.White;
            myDirLight.Direction = new Vector3D(-1, -1, -2);

            viewport.Children.Add(new ModelVisual3D() { Content = myDirLight });

            viewport.Height = 1000;
            viewport.Width = 1000;
            canvas.Width = 1500;
            canvas.Height = 1500;
            canvas.Children.Add(viewport);
        }

        private Dictionary<string, double> Rescale(Dictionary<string, double> eig)
        {
            List<string> keyList = new List<string>(eig.Keys);
            var greatest = double.MinValue;
            for (int i = 0; i < eig.Count; i++)
            {
                if (eig[keyList[i]] > greatest)
                {
                    greatest = eig[keyList[i]];
                }
            }

            for (int i = 0; i < eig.Count; i++)
            {
                eig[keyList[i]] *= (200/greatest);
            }

            return eig;
        }

        private PointCollection ClockWise(PointCollection p)
        {
            PointCollection points = new PointCollection();
            for (int i = p.Count - 1; i > 0; i--)
            {
                points.Add(p[i]);
            }

            return points;
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


            LineSimp(points);
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
            p.Points = LineSimp(PolygonToPointColl(polygon));

           
            return p;
        }

        private Polygon CreatePolygon(GeoJSON.Net.Geometry.Polygon polygon, SolidColorBrush color, bool clockWise)
        {
            Polygon p = new Polygon();
            p.Stroke = Brushes.Black;
            p.Fill = color;
            p.StrokeThickness = 1;
            p.HorizontalAlignment = HorizontalAlignment.Left;
            p.VerticalAlignment = VerticalAlignment.Center;
            ReadGeo rg = new ReadGeo();
            p.Points = ClockWise(LineSimp(PolygonToPointColl(polygon)));

            

            return p;
        }

        private List<Point[]> PolygonTriangulation(Polygon polygon)
        {
            EarClipping earClipping = new EarClipping();

            return TriangleToPoints(earClipping.Triangulate(ConvertPolygon(polygon)));
        }

        private Globals.Polygon ConvertPolygon(Polygon p)
        {
            List<Globals.Point> points = new List<Globals.Point>();
            
            Globals.Polygon polygon = new Globals.Polygon(ConvertPolyToPoints(p));

            return polygon;
        }

        private List<Globals.Point> ConvertPolyToPoints(Polygon p)
        {
            List<Globals.Point> points = new List<Globals.Point>();
            for (int i = 0; i < p.Points.Count; i++)
            {
                points.Add(new Globals.Point(p.Points[i].X, p.Points[i].Y));
            }

            return points;
        }


        private List<Globals.Point> ConvertPointCollToPt(PointCollection p)
        {
            List<Globals.Point> points = new List<Globals.Point>();
            for (int i = 0; i < p.Count; i++)
            {
                points.Add(new Globals.Point(p[i].X, p[i].Y));
            }

            return points;
        }

        private List<Point[]> TriangleToPoints(List<Globals.Triangle> tri)
        {
            List<Point[]> points = new List<Point[]>();
            for (int i = 0; i < tri.Count; i++)
            {
                points.Add(new Point[] { new Point(tri[i].Points[0].X, tri[i].Points[0].Y), 
                                         new Point(tri[i].Points[1].X, tri[i].Points[1].Y), 
                                         new Point(tri[i].Points[2].X, tri[i].Points[2].Y)
                                       });
            }

            return points;
        }
    }
}
