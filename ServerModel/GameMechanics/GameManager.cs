﻿using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace ServerModel.GameMechanics
{
    public class GameManager
    {
        private const float _virusGroupTimerInterval = 100;
        private readonly Timer _virusGroupTimer;
        private readonly List<GameSession> _gameSessions;
        private readonly Dictionary<int, Client> _findGameClients;

        public GameManager()
        {
            _gameSessions = new List<GameSession>();
            _findGameClients = new Dictionary<int, Client>();

            _virusGroupTimer = new Timer(_virusGroupTimerInterval);
            _virusGroupTimer.Elapsed += _virusGroupTimer_Elapsed;
            _virusGroupTimer.Start();
        }

        private void TryToCreateGame()
        {
            if (_findGameClients.Count < 2)
                return;
            Client client1 = _findGameClients.First().Value;
            _findGameClients.Remove(client1.AccountInfo.Id);
            Client client2 = _findGameClients.First().Value;
            _findGameClients.Remove(client2.AccountInfo.Id);
            _gameSessions.Add(new GameSession(new Client[] { client1, client2 }, GameMode.OneByOne));
        }
        private void _virusGroupTimer_Elapsed(object sender, ElapsedEventArgs e) => _gameSessions.ForEach(x => x.UpdateVirusGroup());

        public void ClientReadyToFindGame(Client client)
        {
            _findGameClients.Add(client.AccountInfo.Id, client);
            TryToCreateGame();
        }
    }
}