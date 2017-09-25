using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectUtils {

    public static Rect withHeight(this Rect r, float height) {
        return new Rect(r.x, r.y, r.width, height);
    }

    public static Rect fromHeight(this Rect r, float height) {
        return new Rect(r.x, r.y + height, r.width, r.height - height);
    }

	public static Rect withWidth(this Rect r, float width)
	{
        return new Rect(r.x, r.y, width, r.height);
	}

	public static Rect fromWidth(this Rect r, float width)
	{
        return new Rect(r.x + width, r.y, r.width - width, r.height);
	}

    public static Rect withPadding(this Rect r, float lpad, float rpad, float tpad, float bpad) {
        return new Rect(r.x + lpad, r.y + tpad, r.width - lpad - rpad, r.height - tpad - bpad);
    }

	public static Rect withPadding(this Rect r, float pad)
	{
        return r.withPadding(pad, pad, pad, pad);
	}
}
