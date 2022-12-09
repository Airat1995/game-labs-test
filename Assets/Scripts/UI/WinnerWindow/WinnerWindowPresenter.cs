namespace UI.WinnerWindow
{
    public class WinnerWindowPresenter
    {
        private readonly WinnerWindow _view;

        public WinnerWindowPresenter(WinnerWindow view)
        {
            _view = view;
        }

        public void ShowWinner(bool firstPlayerDead, bool secondPlayerDead)
        {
            string winner;
            if (!firstPlayerDead && secondPlayerDead)
            {
                winner = "First Player Won!";
            }
            else if (firstPlayerDead && !secondPlayerDead)
            {
                winner = "Second Player Won!";
            }
            else
            {
                winner = "DRAW!";
            }
            _view.WinnerText.text = winner;
            _view.gameObject.SetActive(true);
        }
    }
}