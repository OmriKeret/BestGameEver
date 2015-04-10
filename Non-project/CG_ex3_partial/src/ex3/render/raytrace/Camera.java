package ex3.render.raytrace;

import java.util.Map;

import ex3.parser.StringUtils;
import math.Axis3D;
import math.Point3D;
import math.Ray;
import math.Vec;


/**
 * Represents the scene's camera.
 * 
 */
public class Camera implements IInitable{

    private Point3D _P0,_Pc;
    private double _d, _Rx,_Ry, R;
    private  Axis3D _axis3D;

    public void setResolution(double width, double hight){
        _Rx = width;
        _Ry = hight;
    }

	public void init(Map<String, String> attributes) {
        try {
            _P0 = StringUtils.String2Point(attributes.get("eye"));
            Vec Vup = StringUtils.String2Vector(attributes.get("up-direction"));
            Vec Vto = StringUtils.String2Vector(attributes.get("direction"));
           _axis3D = new Axis3D(Vto,Vup);

            _d = Double.parseDouble(attributes.get("screen-dist"));
        }
        catch (Exception e){
            System.out.println(e);
        }
	}
	
	/**
	 * Transforms image xy coordinates to view pane xyz coordinates. Returns the
	 * ray that goes through it.
	 * 
	 * @param x - x cordinate of the object on the plane
	 * @param y - y cordinate of the object on the plane
     * @param height - height of the plane
     * @param width - width of the plane
	 * @return
	 */
	
	
	public Ray constructRayThroughPixel(double x, double y, double height, double width) {		
        //Pc = P0+dVto
        _Pc = _P0.addVector(Vec.scale(_d,_axis3D.get_Vto()));
        R = width/_Rx;
        Point3D P = calculateP(x,y);
        Vec V = calculateV(P);
        //The wanted ray is P0+tV
		return new Ray(_P0,V);
	}

    /**
     * Calculate P via the formula:
     * //P = Pc+(x-[Rx/2])RVright-(y-[Ry/2])RVup
     * @param x
     * @param y
     * @return
     */
    private Point3D calculateP(double x,double y){
        Point3D P = new Point3D(_Pc);
        P.addVector(Vec.scale((x - Math.floor(_Rx/2))*R, _axis3D.get_Vright()));
        P.subtractVector(Vec.scale((y - Math.floor(_Ry/2))*R, _axis3D.get_Vup()));
        return P;
    }

    /**
     * Calculate V from the point P
     * @param P
     * @return
     */
    private Vec calculateV (Point3D P){
        Vec V = _P0.GetVectorToPoint(P);
        V.normalize();
        return V;
    }

	
	
	
	
	
	
	
}
