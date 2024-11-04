using UnityEngine;

public class BaseStructureScript : BuildingBase
{
    #region Unity Runtime Functions
/*    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }*/
    #endregion
    public override void OnIntract() {
        gameManager.StartHubManagementGame();
    }

    public override void SetSprite() {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(global::System.Single damageAmount, global::System.Int32 knockBackF, GameObject damageFrom) {
        base.TakeDamage(damageAmount, knockBackF, damageFrom);
    }

    protected override void Awake() {
        base.Awake();
    }

    protected override void SetStage(State active) {
        base.SetStage(active);
    }

    protected override void Stage_Build1() {
        base.Stage_Build1();
    }

    protected override void Stage_Build2() {
        base.Stage_Build2();
    }

    protected override void Stage_Build3() {
        base.Stage_Build3();
    }

    protected override void Stage_Node() {
        base.Stage_Node();
    }

    protected override void UpgradeStage() {
        base.UpgradeStage();
    }


}
