public static class GlobalVars
{
    #region Movement
    public const string HorizontalAxis = "Horizontal";
    public const string VerticalAxis = "Vertical";
    public const string Jump = "Jump";
    public const string Fire1 = "Fire1";
    #endregion
    
    public static readonly string[] MortalTags = {"Player", "Character", "Mortal", "Enemy", "Live", "Alive"};
    public static readonly string[] BulletIgnoreTags = {"CameraConfiner", "Respawn", "Collider", "Trigger", "EnemyStopper"};
    public static readonly string[] EnemyStopOnEnter = {"Ground", "EnemyStopper"};
}
