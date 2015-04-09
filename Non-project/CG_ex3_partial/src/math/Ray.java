package math;

public class Ray {

	// point of origin
	public Point3D p;
	// ray direction
	public Vec v;
	
	/**
	 * constructs a new ray
	 * @param p - point of origin
	 * @param v - ray direction
	 */

	public Ray(Point3D p, Vec v) {
		this.p = p;
		this.v = v;
	}

    /**
     * constructs an empty ray
     */
    public Ray(){
        p = new Point3D();
        v = new Vec();
    }


}
