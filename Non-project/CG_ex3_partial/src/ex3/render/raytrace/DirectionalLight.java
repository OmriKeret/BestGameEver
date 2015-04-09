package ex3.render.raytrace;

import ex3.parser.StringUtils;
import math.Point3D;
import math.Vec;

import java.util.Map;

/**
 * Created by 7 on 09/04/2015.
 */

public class DirectionalLight extends Light {

    private Vec _direction;

    @Override
    public void init(Map<String, String> attributes){
        try {
            super.init(attributes);
            _direction = StringUtils.String2Vector(attributes.get("direction"));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    @Override
    public double get_intensity(Point3D i_point) {
        return _intensity;
    }
}
