﻿using GameCore.Model;
using GameCore.Tools;
using System;
using System.Collections.Generic;
using System.Timers;

namespace ServerModel.GameMechanics
{
    public class VirusGroup
    {
        private readonly float _speed;
        private readonly Road _road;
        private readonly Player _owner;

        public readonly TimeSpan DrivingTime;
        public readonly DateTime Start;
        public readonly int VirusCount;
        public BacteriumModel EndBacterium => _road.End;

        public VirusGroup() { }
        public VirusGroup(float speed, Road road, Player owner, int virusCount)
        {
            _speed = speed;
            _road = road ?? throw new ArgumentNullException(nameof(road));
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            DrivingTime = TimeSpan.FromSeconds(road.Length / _speed);
            Start = DateTime.Now;
            VirusCount = virusCount;
        }

        public VirusGroupData VirusGroupData => new VirusGroupData(_road.Id, _road.Start.Id, _road.Start.Data.VirusCount, _road.End.Id);
    }
}