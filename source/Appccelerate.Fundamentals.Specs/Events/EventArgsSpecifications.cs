//-------------------------------------------------------------------------------
// <copyright file="EventArgsSpecifications.cs" company="Appccelerate">
//   Copyright (c) 2008-2015
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.Events
{
    using System;

    using FluentAssertions;

    using Xbehave;

    public class GenericEventArguments
    {
        [Scenario]
        public void FireGenericEventArgs(
            Publisher publisher,
            Subscriber subscriber)
        {
            const int Value = 42;

            "establish a publisher firing an event with generic event args"._(() =>
                {
                    publisher = new Publisher();
                });

            "establish a subscriber listening to the event of the publisher"._(() =>
                {
                    subscriber = new Subscriber();

                    subscriber.RegisterEvent(publisher);
                });

            "when the publisher fires the event"._(() =>
                {
                    publisher.FireEvent(Value);
                });

            "it should pass value to event handler"._(() =>
                {
                    subscriber.ReceivedValue
                        .Should().Be(Value);
                });
        }

        public class Publisher
        {
            public event EventHandler<EventArgs<int>> AnEvent = delegate { };

            public void FireEvent(int i)
            {
                this.AnEvent(this, new EventArgs<int>(i));
            }
        }

        public class Subscriber
        {
            public int ReceivedValue { get; private set; }

            public void RegisterEvent(Publisher publisher)
            {
                publisher.AnEvent += (sender, eventArgs) => this.ReceivedValue = eventArgs.Value;
            }
        }
    }
}