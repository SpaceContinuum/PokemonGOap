public class GoToHospital : GAction {
    public override bool PrePerform() {

        return true;
    }

    public override bool PostPerform() {

        return true;
    }

    public override void Reset()
    {
        afterEffects[0] = new WorldState("isWaiting", 0);
    }
}
