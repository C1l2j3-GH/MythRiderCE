public abstract class L1BossAtkBase
{
    public abstract void EnterForm(L1BossAI l1Boss);

    public abstract void UpdateForm(L1BossAI l1Boss);

    public abstract void ExitForm(L1BossAI l1Boss);

    public abstract void OnCollisionEnter(L1BossAI l1Boss);

    public abstract void OnTriggerEnter(L1BossAI l1Boss);
}
