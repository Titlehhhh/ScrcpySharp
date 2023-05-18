using Java.Lang;

static class Ext
{
	public static Class GetClass(this System.Type type) => Class.FromType(type);
}
