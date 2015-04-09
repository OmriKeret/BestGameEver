package math;

/**
 * Created by 7 on 09/04/2015.
 */
public class Axis3D {

    private Vec _Vup;
    private Vec _Vto;
    private Vec _Vright;


    public Vec get_Vup() {
        return _Vup;
    }
    public Vec get_Vto() {
        return _Vto;
    }
    public Vec get_Vright() {
        return _Vright;
    }

    public Axis3D(Vec i_up,Vec i_to,Vec i_right){
        _Vright = i_right;
        _Vto = i_to;
        _Vup = i_up;
    }

    public Axis3D(Vec i_up,Vec i_to){
        _Vto = i_to;
        _Vup = i_up;
        PrependiculerAndNormalized();
    }

    /**
     * Make all 3 vectors perpendicular to each-other and normalize them.
     */
    public void PrependiculerAndNormalized(){
        _Vto.normalize();
        _Vright = Vec.crossProd(_Vto, _Vup);
        _Vright.normalize();
        _Vup = Vec.crossProd(_Vto, _Vright);
        _Vup.normalize();
    }
}
