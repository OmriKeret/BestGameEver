package ex3.parser;

import math.Point3D;
import math.Vec;

import java.awt.Color;
import java.io.File;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by 7 on 08/04/2015.
 */
public class StringUtils {

    private static Pattern pointPattern = Pattern.compile("-?[0-9.]+ -?[0-9.]+ -?[0-9.]+");
    private static Matcher pointMatcher;
    private static Pattern elementPattern = Pattern.compile("-?[0-9.]+");
    private static Matcher elementMatcher;

    /**
     * Takes a string representing a point and converts it to a point
     * @param i_stringPoint
     * @return
     * @throws Exception
     */
    public static Point3D String2Point(String i_stringPoint) throws Exception{
        if (i_stringPoint==null)
            return new Point3D();
        pointMatcher = pointPattern.matcher(i_stringPoint);
        if (!pointMatcher.matches())
            throw new Exception("The string "+i_stringPoint+" is not in a vector pattern!");
        elementMatcher = elementPattern.matcher(i_stringPoint);
        String[] e = i_stringPoint.split(" ");
        return new Point3D(Double.parseDouble(e[0]),Double.parseDouble(e[1]),Double.parseDouble(e[2]));

    }

    /**
     * Takes a string representing a point and converts it to a vector
     * The method takes in account that a vector and a point are represented by x y and z
     * @param i_stringVector
     * @return
     * @throws Exception
     */
    public static Vec String2Vector(String i_stringVector) throws Exception{
        Point3D P0 = new Point3D();
        Point3D P1 = String2Point(i_stringVector);
        return P0.GetVectorToPoint(P1);
    }
    
    public static Color string2Color(String i_stringVector)throws Exception {
    	Point3D point = String2Point(i_stringVector);
    	return new Color((int)(point.x*255),(int)(point.y*255),(int)(point.z*255));
    
    }
    
    public static File string2File(String fileName) {
    	if(fileName == null) {
    
    		//return default
    		return null;
    	}
    	File file = new File(fileName);
    	if(!file.isFile()) {
    		System.err.println("Given texture file doesn't exists: " + fileName);
    		return null;
    	}
    	return file;
    
    }
    public static double string2Number(String number) {
    	if(number == null) {
    		
    		//return default
    		return 10;
    	}
    	Scanner s = new Scanner(number);
    	double x = s.nextDouble();
    	s.close();	
    	return x;
    
    }

}
