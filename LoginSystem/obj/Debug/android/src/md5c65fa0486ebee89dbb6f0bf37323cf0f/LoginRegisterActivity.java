package md5c65fa0486ebee89dbb6f0bf37323cf0f;


public class LoginRegisterActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("LoginSystem.LoginRegisterActivity, LoginSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginRegisterActivity.class, __md_methods);
	}


	public LoginRegisterActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoginRegisterActivity.class)
			mono.android.TypeManager.Activate ("LoginSystem.LoginRegisterActivity, LoginSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
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
