package Shapes;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Ray;

import java.util.LinkedList;
import java.util.List;
import java.util.Map;

/**
 * Created by 7 on 10/04/2015.
 */
public class ConvexPolygon extends Ishape{
    List<Triangle> triangleList;

    public ConvexPolygon(Map<String,String> attributes)throws Exception{
        //get all the points
        int pointNumber = 0;
        List<Point3D> points = new LinkedList<Point3D>();
        while (attributes.containsKey("p"+pointNumber)){
            points.add(StringUtils.String2Point(attributes.get("p"+pointNumber)));
            pointNumber++;
        }
        if (pointNumber<2)
            throw new Exception("There are too few points to make a polygon");
        //for each 3 points, create a triangle
        for (int i = 0;i<points.size()-2;i++){
            for (int j=i+1;j<points.size()-1;j++){
                for (int k=j+1;j<points.size();k++){
                    triangleList.add(new Triangle(points.get(i),points.get(j),points.get(k)));
                }
            }

        }
    }


    @Override
    public Point3D intersectWithRay(Ray ray) {
        //find all intersection points
        List<Point3D> points = new LinkedList<Point3D>();
        for (Triangle t : triangleList){
            Point3D point3D = t.intersectWithRay(ray);
            if (point3D!=null)
                points.add(point3D);
        }

        //find who is the closest to ray's origin
        double minDistance = Double.MAX_VALUE;
        Point3D minPoint = null;
        for (Point3D p : points){
            double currentDistance = p.distance(ray.p);
            if (currentDistance<minDistance){
                minDistance = currentDistance;
                minPoint = p;
            }
        }
        return minPoint;
    }
}
