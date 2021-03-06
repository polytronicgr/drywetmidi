﻿using Melanchall.DryWetMidi.Smf;
using Melanchall.DryWetMidi.Smf.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Melanchall.DryWetMidi.Tests.Smf.Interaction
{
    [TestClass]
    public class TempoMapManagerTests
    {
        [TestMethod]
        [Description("Manage new tempo map without specified time division.")]
        public void Manage_New_WithoutTimeDivision()
        {
            using (var tempoMapManager = new TempoMapManager())
            {
                var tempoMap = tempoMapManager.TempoMap;

                Assert.AreEqual(TicksPerQuarterNoteTimeDivision.DefaultTicksPerQuarterNote,
                                ((TicksPerQuarterNoteTimeDivision)tempoMap.TimeDivision).TicksPerQuarterNote);
                Assert.IsFalse(tempoMap.Tempo.Values.Any());
                Assert.IsFalse(tempoMap.TimeSignature.Values.Any());
            }
        }

        [TestMethod]
        [Description("Manage new tempo map with the specified time division.")]
        public void Manage_New_WithTimeDivision()
        {
            using (var tempoMapManager = new TempoMapManager(new TicksPerQuarterNoteTimeDivision(100)))
            {
                var tempoMap = tempoMapManager.TempoMap;

                Assert.AreEqual(100, ((TicksPerQuarterNoteTimeDivision)tempoMap.TimeDivision).TicksPerQuarterNote);
                Assert.IsFalse(tempoMap.Tempo.Values.Any());
                Assert.IsFalse(tempoMap.TimeSignature.Values.Any());
            }
        }

        [TestMethod]
        [Description("Replace a tempo map with the default one.")]
        public void ReplaceTempoMap_ByDefault()
        {
            using (var tempoMapManager = new TempoMapManager())
            {
                tempoMapManager.SetTempo(100, new Tempo(10));
                tempoMapManager.SetTempo(300, new Tempo(100));

                tempoMapManager.ReplaceTempoMap(TempoMap.Default);

                var tempoMap = tempoMapManager.TempoMap;

                Assert.AreEqual(TicksPerQuarterNoteTimeDivision.DefaultTicksPerQuarterNote,
                                ((TicksPerQuarterNoteTimeDivision)tempoMap.TimeDivision).TicksPerQuarterNote);
                Assert.IsFalse(tempoMap.Tempo.Values.Any());
                Assert.IsFalse(tempoMap.TimeSignature.Values.Any());
            }
        }
    }
}
