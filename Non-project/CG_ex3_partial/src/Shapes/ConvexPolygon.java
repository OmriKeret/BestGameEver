package Shapes;

import math.Point3D;
import math.Ray;
import math.Vec;

public class Convexpolygon implements Ishape {
	Point3D a;
	Point3D b;
	Point3D c;
	Point3D d;
	Point3D e;
	Point3D f;
	
	public Convexpolygon(Point3D a, Point3D b, Point3D c, Point3D d, Point3D e,Point3D f) {
		this.a = a;
		this.b = b;
		this.c = c;
		this.d = d;
		this.e = e;
		this.f = f;
	}
	}
	@Override
	public Point3D intersectWithRay(Ray ray) {
		
		Vec normal = Vec.crossProd(b.GetVectorToPoint(c), b.GetVectorToPoint(a));
		double t = (normal.dotProd(ray.p.GetVectorToPoint(a))) / (normal.dotProd(ray.v));
		if(t < 0 || normal.dotProd(ray.v) == 0) {
			return null;
		}
		Vec temp = new Vec(ray.v);
		temp.scale(t);
		Point3D intersactionPoint = ray.p.addVector(temp);
		
		int   i, j = 5 ;
		boolean  oddNodes = false;

	  for (i=0; i < 6; i++) {
	    if ((polyY[i] < y && polyY[j] >= y ||   polyY[j] < y && polyY[i]>=y ) &&  (polyX[i] <= x || polyX[j] <= x)) {
	      oddNodes ^= (polyX[i]+(y-polyY[i])/(polyY[j]-polyY[i])*(polyX[j]-polyX[i])<x); 
	      }
	    j=i; 
	    }

			  return oddNodes; }
		return null;
	}
	

}
