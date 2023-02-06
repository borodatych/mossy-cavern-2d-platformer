public class GlobalVars
{
    #region Movement
    public const string HORIZONTAL_AXIS = "Horizontal";
    public const string VERTICAL_AXIS = "Vertical";
    public const string JUMP = "Jump";
    public const string FIRE_1 = "Fire1";
    #endregion
    
    public static string[] MortalTags = {"Player", "Character", "Mortal", "Enemy", "Live", "Alive"};
    public static string[] BulletIgnoreTags = {"CameraConfiner", "Respawn", "Collider", "Trigger", "EnemyStopper"};
    public static readonly string[] EnemystopOnEnter = {"Ground", "EnemyStopper"};
}
