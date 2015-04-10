package Shapes;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Ray;
import math.Vec;

import java.util.Map;

public class Triangle extends Ishape{

	Point3D a;
	Point3D b;
	Point3D c;
	
	public Triangle(Map<String,String> attributes) throws Exception{
        init(attributes);
		a = StringUtils.String2Point(attributes.get("p0"));
		b = StringUtils.String2Point(attributes.get("p1"));
		c = StringUtils.String2Point(attributes.get("p2"));
	}

    public Triangle(Point3D i_a,Point3D i_b,Point3D i_c){
        a=i_a;
        b=i_b;
        c=i_c;
    }
	
	@Override
	public Point3D intersectWithRay(Ray ray) {
		
		//geting the triangle plane
		Vec v1 = new Vec(b.GetVectorToPoint(a));
		Vec v2 = new Vec(c.GetVectorToPoint(a));
		Vec v3 = Vec.crossProd(v1, v2);
		
		if(v3.length() == 0) {
			return null;
		}
		
		Vec vecBetween = new Point3D(ray.p).GetVectorToPoint(a);
		Double d = new Vec(v3).dotProd(vecBetween);		
		Double f = new Vec(v3).dotProd(ray.v);
		if( d < 0.000000001) {
			return null;
		}
		
		double r = d / f;
		if( Math.abs(r) < 0.0) {
			return null;
		}
		Vec vTemp = new Vec(ray.v);
		vTemp.scale(r);
		Point3D pointOfIntersaction  = ray.p.addVector(vTemp);

		if(Math.abs(pointOfIntersaction.GetVectorToPoint(a).dotProd(v3)) > 0.001) {
			return null;
		}
		if(!PointInTriangle(pointOfIntersaction,v1,v2,v3)){
			return null;
		}
		
	    return pointOfIntersaction;
	}

    private boolean PointInTriangle(Point3D p, Vec v1, Vec v2, Vec v3)
	{	
		double    uu, uv, vv, wu, wv, D,s,t;
		
	    uu = v1.lengthSquared();
	    uv = v1.dotProd(v2);
	    vv = v2.lengthSquared();
	    Vec w = p.GetVectorToPoint(a);
	    wu = w.dotProd(v1);
	    wv = w.dotProd(v2);
	    D = uv * uv - uu * vv;

	    // get and test parametric coords

	    s = (uv * wv - vv * wu) / D;
	    if (s < 0.0 || s > 1.0)         // point is outside of triangle
	        return false;
	    t = (uv * wu - uu * wv) / D;
	    if (t < 0.0 || (s + t) > 1.0)  // point is outside triangle
	        return false;

	    return true;                       // point is in Triangle
	}
}
