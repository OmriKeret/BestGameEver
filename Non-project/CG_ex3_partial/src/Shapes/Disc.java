package Shapes;

import ex3.parser.StringUtils;
import math.Axis3D;
import math.Point3D;
import math.Ray;
import math.Vec;

import java.util.Map;
import java.util.SplittableRandom;

public class Disc extends Ishape {

	Vec _normal;
	double _radius;
	Point3D _center;
	
	public Disc(Map<String,String> attributes) throws Exception{
		init(attributes);
        _normal = StringUtils.String2Vector(attributes.get("normal"));
		_radius = StringUtils.string2Number(attributes.get("radius"));
		_center = StringUtils.String2Point(attributes.get("center"));
	}
	@Override
	public Point3D intersectWithRay(Ray ray) {
        Vec vecToCenter = ray.p.GetVectorToPoint(_center);
        double angleToNormal = vecToCenter.angle(_normal);
        //A vector that will hit the disc if it begins in ray origin point
        Vec normalHitDisc = new Vec(_normal);
        normalHitDisc.scale(vecToCenter.length()*Math.cos(angleToNormal));
        //A vector in ray's direction that will hit the disc
        double angleFromNormal = normalHitDisc.angle(ray.v);
        Vec rayHit = new Vec(ray.v);
        rayHit.scale(normalHitDisc.length()/Math.cos(angleFromNormal));

        Point3D hit = ray.p.addVector(rayHit);
        //check if this point is on the disc
        if (_center.distance(hit)<=_radius)
            return hit;

        return null;
		
	}
	

}
