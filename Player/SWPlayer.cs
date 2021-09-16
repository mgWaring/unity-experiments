
public class SWPlayer
{

    private string id { get; }
    private Character character { get; }
    private Gun gun { get; }
    private Hat hat { get; }
    private AmmoBelt ammoBelt { get; }
    //private SoundPack soundPack { get; }
    //private HUD hud { get; }

    //try to invert the dependencies here as much as possible
    //pass in the required objects at construction
    //define type constrainst using interfaces not classes
    public SWPLayer(
        string _id,
        Character _character,
        Gun _gun,
        Hat _hat,
        AmmoBelt _ammoBelt
    //SoundPack _soundPack,
    //HUD _hud
    )
    {
        id = _id;
        character = _character;
        gun = _gun;
        hat = _hat;
        ammoBelt = _ammoBelt;
        //soundPack = _soundPack;
        //hud = _hud;

    }
}