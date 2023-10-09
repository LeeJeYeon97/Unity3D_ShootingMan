using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Loading : UI_Scene
{
    enum Sliders
    {
        Slider_LoadingBar,
    }

    public Slider slider;
    public override void Init()
    {
        Bind<Slider>(typeof(Sliders));
        slider = Get<Slider>((int)Sliders.Slider_LoadingBar);
    }
}
