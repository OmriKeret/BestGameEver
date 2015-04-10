package ex3.render.raytrace;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Vec;

import java.awt.*;
import java.util.Map;

/**
 * Created by 7 on 09/04/2015.
 */
public class SpotLight extends Light {
    Vec _direction;
    Point3D _position;
    double _kc,_kl,_kq;

    @Override
    public void init(Map<String, String> attributes) {
        try {
            super.init(attributes);
            _direction = StringUtils.String2Vector(attributes.get("dir"));
            _position = StringUtils.String2Point(attributes.get("pos"));
            _kc = Double.parseDouble(attributes.get("kc"));
            _kl = Double.parseDouble(attributes.get("kl"));
            _kq = Double.parseDouble(attributes.get("kq"));
        }
        catch (Exception e){
            e.printStackTrace();
        }

    }

    @Override
    public double get_intensity(Point3D i_point) {
        Vec D = _position.GetVectorToPoint(i_point);
        double up = D.dotProd(_direction)*_intensity;
        double down = _kc+_kl*D.length()+_kq*D.length()*D.length();
        return (up/down);
    }

    @Override
    public Vec getDirection(Point3D i_location) {
        return i_location.GetVectorToPoint(_position);
    }

}
