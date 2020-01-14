using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars1
{
    //CLOSED, LISTEN, SYN_SENT, SYN_RCVD, ESTABLISHED, CLOSE_WAIT, LAST_ACK, FIN_WAIT_1, FIN_WAIT_2, CLOSING, TIME_WAIT
    enum State
    {
        ERROR,
        CLOSED,
        LISTEN,
        SYN_SENT,
        SYN_RCVD,
        ESTABLISHED,
        CLOSE_WAIT,
        LAST_ACK,
        FIN_WAIT_1,
        FIN_WAIT_2,
        CLOSING,
        TIME_WAIT,
    }
    //APP_PASSIVE_OPEN, APP_ACTIVE_OPEN, APP_SEND, APP_CLOSE, APP_TIMEOUT, RCV_SYN, RCV_ACK, RCV_SYN_ACK, RCV_FIN, RCV_FIN_ACK
    enum Event
    {
        APP_PASSIVE_OPEN,
        APP_ACTIVE_OPEN,
        APP_SEND,
        APP_CLOSE,
        APP_TIMEOUT,
        RCV_SYN,
        RCV_ACK,
        RCV_SYN_ACK,
        RCV_FIN,
        RCV_FIN_ACK,
    }
    public class TCP
    {
        //https://www.codewars.com/kata/54acc128329e634e9a000362/train/csharp
        public static string TraverseStates(string[] events)
        {
            try
            {
                var tuples = SetupRelationShips();
                var eList = new List<Event>();
                var state = State.CLOSED;

                foreach (var s in events)
                {
                    eList.Add((Event)Enum.Parse(typeof(Event), s));
                }
                foreach (var e in eList)
                {
                    state = tuples.Where(x => x.Item1 == state && x.Item2 == e).Select(y => y.Item3).FirstOrDefault();

                    if (state == State.ERROR)
                        throw new Exception("ERROR");
                }

                return state.ToString();
            }
            catch(Exception e)
            {
                return State.ERROR.ToString();
            }
        }
        private static List<Tuple<State, Event, State>> SetupRelationShips()
        {
            var tuples = new List<Tuple<State, Event, State>>();

            //CLOSED
            tuples.Add(new Tuple<State, Event, State>(State.CLOSED, Event.APP_PASSIVE_OPEN, State.LISTEN));
            tuples.Add(new Tuple<State, Event, State>(State.CLOSED, Event.APP_ACTIVE_OPEN, State.SYN_SENT));
            //LISTEN
            tuples.Add(new Tuple<State, Event, State>(State.LISTEN, Event.RCV_SYN, State.SYN_RCVD));
            tuples.Add(new Tuple<State, Event, State>(State.LISTEN, Event.APP_SEND, State.SYN_SENT));
            tuples.Add(new Tuple<State, Event, State>(State.LISTEN, Event.APP_CLOSE, State.CLOSED));
            //SYN_RCVD
            tuples.Add(new Tuple<State, Event, State>(State.SYN_RCVD, Event.APP_CLOSE, State.FIN_WAIT_1));
            tuples.Add(new Tuple<State, Event, State>(State.SYN_RCVD, Event.RCV_ACK, State.ESTABLISHED));
            //SYN_SENT
            tuples.Add(new Tuple<State, Event, State>(State.SYN_SENT, Event.RCV_SYN, State.SYN_RCVD));
            tuples.Add(new Tuple<State, Event, State>(State.SYN_SENT, Event.RCV_SYN_ACK, State.ESTABLISHED));
            tuples.Add(new Tuple<State, Event, State>(State.SYN_SENT, Event.APP_CLOSE, State.CLOSED));
            //ESTABLISHED
            tuples.Add(new Tuple<State, Event, State>(State.ESTABLISHED, Event.APP_CLOSE, State.FIN_WAIT_1));
            tuples.Add(new Tuple<State, Event, State>(State.ESTABLISHED, Event.RCV_FIN, State.CLOSE_WAIT));
            //FIN_WAIT
            tuples.Add(new Tuple<State, Event, State>(State.FIN_WAIT_1, Event.RCV_FIN, State.CLOSING));
            tuples.Add(new Tuple<State, Event, State>(State.FIN_WAIT_1, Event.RCV_FIN_ACK, State.TIME_WAIT)); //
            tuples.Add(new Tuple<State, Event, State>(State.FIN_WAIT_1, Event.RCV_ACK, State.FIN_WAIT_2));
            //CLOSING
            tuples.Add(new Tuple<State, Event, State>(State.CLOSING, Event.RCV_ACK, State.TIME_WAIT)); //
            //FIN_WAIT 2
            tuples.Add(new Tuple<State, Event, State>(State.FIN_WAIT_2, Event.RCV_FIN, State.TIME_WAIT)); //
            //TIME_WAIT
            tuples.Add(new Tuple<State, Event, State>(State.TIME_WAIT, Event.APP_TIMEOUT, State.CLOSED));
            //CLOSE_WAIT
            tuples.Add(new Tuple<State, Event, State>(State.CLOSE_WAIT, Event.APP_CLOSE, State.LAST_ACK));
            //LAST_ACK
            tuples.Add(new Tuple<State, Event, State>(State.LAST_ACK, Event.RCV_ACK, State.CLOSED));

            return tuples;
        }
    }

    public class TcpTest
    {
        [Test]
        public void Test01()
        {
            Assert.AreEqual("CLOSE_WAIT", TCP.TraverseStates(new[] { "APP_ACTIVE_OPEN", "RCV_SYN_ACK", "RCV_FIN" }));
        }
        [Test]
        public void Test02()
        {
            Assert.AreEqual("ESTABLISHED", TCP.TraverseStates(new[] { "APP_PASSIVE_OPEN", "RCV_SYN", "RCV_ACK" }));
        }
        [Test]
        public void Test03()
        {
            Assert.AreEqual("LAST_ACK", TCP.TraverseStates(new[] { "APP_ACTIVE_OPEN", "RCV_SYN_ACK", "RCV_FIN", "APP_CLOSE" }));
        }
        [Test]
        public void Test04()
        {
            Assert.AreEqual("SYN_SENT", TCP.TraverseStates(new[] { "APP_ACTIVE_OPEN" }));
        }
        [Test]
        public void Test05()
        {
            Assert.AreEqual("ERROR", TCP.TraverseStates(new[] { "APP_PASSIVE_OPEN", "RCV_SYN", "RCV_ACK", "APP_CLOSE", "APP_SEND" }));
        }
    }
}