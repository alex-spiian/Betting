using Pools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VitalRouter;

public class MainScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _currentBetLabel;
    [SerializeField] private TextMeshProUGUI _currentMoneyLabel;
    [SerializeField] private Transform _rewardLabelsContainer;
    [SerializeField] private Button _topUpButton;
    [SerializeField] private RewardLabel _rewardLabelPrefab;

    private readonly CompositeDisposable _subscriptions = new();
    private MonoBehaviourPool<RewardLabel> _pool;

    private void Awake()
    {
        _pool = new MonoBehaviourPool<RewardLabel>(_rewardLabelPrefab, _rewardLabelsContainer);
        _topUpButton.onClick.AddListener(OnTopUp);
        _subscriptions.Add(Router.Default.Subscribe<GotTargetEvent>(OnGotTarget));
    }

    private void OnDestroy()
    {
        _topUpButton.onClick.RemoveListener(OnTopUp);
        _subscriptions?.Dispose();
    }

    public void RefreshBetAmount(float amount)
    {
        _currentBetLabel.text = amount.ToString();
    }

    public void RefreshMoneyAmount(float amount)
    {
        _currentMoneyLabel.text = amount.ToString();
    }

    private void OnTopUp()
    {
        ScreensManager.OpenScreen<PaymentScreen>();
    }

    private void OnGotTarget(GotTargetEvent eventData, PublishContext publishContext)
    {
        var rewardLabel = _pool.Take();
        rewardLabel.Show(eventData.Multiplier, eventData.CurrentBet, OnRewardHides);
    }

    private void OnRewardHides(RewardLabel rewardLabel)
    {
        _pool.Release(rewardLabel);
    }
}