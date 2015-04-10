package ex3.render.raytrace;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Vec;

import java.awt.*;
import java.util.Map;

/**
 * Created by 7 on 09/04/2015.
 */
public class PointLight extends Light {
    private Point3D _position;
    private double _kc,_kl,_kq;

    @Override
    public void init(Map<String, String> attributes){
        try {
            super.init(attributes);
            _kc = Double.parseDouble(attributes.get("kc"));
            _kl = Double.parseDouble(attributes.get("kl"));
            _kq = Double.parseDouble(attributes.get("kq"));

            _position = StringUtils.String2Point(attributes.get("pos"));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public double get_intensity(Point3D i_point) {
        double distance = _position.distance(i_point);
        double down = _kc+_kl*distance+_kq*distance*distance;
        return (_intensity/down);
    }

    @Override
    public Vec getDirection(Point3D i_location) {
        return i_location.GetVectorToPoint(_position);
    }
}
