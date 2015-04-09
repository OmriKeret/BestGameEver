package math;

import java.awt.*;
import java.util.Scanner;

public class Point3D{
	public double x, y, z;


	public Point3D(String v) {
		Scanner s = new Scanner(v);
		x = s.nextDouble();
		y = s.nextDouble();
		z = s.nextDouble();
		s.close();
	}

    public Point3D(){
        x=0;
        y=0;
        z=0;
    }

    /**
     * creates a point from given x y and z
     * @param i_x
     * @param i_y
     * @param i_z
     */
    public Point3D(double i_x,double i_y,double i_z){
        x = i_x;
        y=i_y;
        z=i_z;
    }

    /**
     * copy constructor
     * @param i_point
     */
    public Point3D(Point3D i_point){
        x = i_point.x;
        y = i_point.y;
        z = i_point.z;
    }

    /**
     * Return the vector between this point and a given point
     * @param i_point
     * @return Vec
     */
    public Vec GetVectorToPoint(Point3D i_point){
        return new Vec(i_point.x-x,i_point.y-y,i_point.z-z);
    }

    /**
     * Add this point to a given vector
     * @param i_vector
     * @return Point3D
     */
    public Point3D addVector(Vec i_vector){
        return new Point3D(x+i_vector.x,y+i_vector.y,z+i_vector.z);
    }

    public Point3D subtractVector(Vec i_vector){
        return addVector(Vec.negate(i_vector));
    }

    /**
     * Calculates the distance between 2 points
     * @param i_point
     * @return
     */
    public double distance(Point3D i_point){
        return GetVectorToPoint(i_point).length();
    }

    public boolean equals(Point3D i_point){
        return (((x== i_point.x)&&(y==i_point.y))&&(z==i_point.z));
    }

}

