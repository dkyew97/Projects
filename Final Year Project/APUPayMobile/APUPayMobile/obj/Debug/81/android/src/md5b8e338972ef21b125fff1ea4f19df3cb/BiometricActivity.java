package md5b8e338972ef21b125fff1ea4f19df3cb;


public class BiometricActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("APUPayMobile.BiometricActivity, APUPayMobile", BiometricActivity.class, __md_methods);
	}


	public BiometricActivity ()
	{
		super ();
		if (getClass () == BiometricActivity.class)
			mono.android.TypeManager.Activate ("APUPayMobile.BiometricActivity, APUPayMobile", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
