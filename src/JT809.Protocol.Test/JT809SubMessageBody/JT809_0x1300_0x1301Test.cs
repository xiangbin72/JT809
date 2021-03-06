﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT809.Protocol;
using JT809.Protocol.Extensions;
using JT809.Protocol.MessageBody;
using JT809.Protocol.Exceptions;
using JT809.Protocol.SubMessageBody;
using JT809.Protocol.Enums;
using JT809.Protocol.Internal;

namespace JT809.Protocol.Test.JT809SubMessageBody
{
    public class JT809_0x1300_0x1301Test
    {
        private JT809Serializer JT809Serializer = new JT809Serializer();
        private JT809Serializer JT809_2019_Serializer = new JT809Serializer(new DefaultGlobalConfig() { Version = JT809Version.JTT2019 });
        [Fact]
        public void Test1()
        {
            JT809_0x1300_0x1301 jT809_0x1300_0x1301 = new JT809_0x1300_0x1301
            {
                ObjectID = "111",
                InfoContent = "22ha22",
                InfoID = 1234,
                ObjectType = JT809_0x1301_ObjectType.当前连接的下级平台
            };
            var hex = JT809Serializer.Serialize(jT809_0x1300_0x1301).ToHexString();
            //"01 31 31 31 00 00 00 00 00 00 00 00 00 00 00 04 D2 00 00 00 06 32 32 68 61 32 32"
            Assert.Equal("01313131000000000000000000000004D200000006323268613232", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "01 31 31 31 00 00 00 00 00 00 00 00 00 00 00 04 D2 00 00 00 06 32 32 68 61 32 32".ToHexBytes();
            JT809_0x1300_0x1301 jT809_0x1300_0x1301 = JT809Serializer.Deserialize<JT809_0x1300_0x1301>(bytes);
            Assert.Equal("111", jT809_0x1300_0x1301.ObjectID);
            Assert.Equal("22ha22", jT809_0x1300_0x1301.InfoContent);
            Assert.Equal((uint)1234, jT809_0x1300_0x1301.InfoID);
            Assert.Equal(JT809_0x1301_ObjectType.当前连接的下级平台, jT809_0x1300_0x1301.ObjectType);
        }

        [Fact]
        public void Test_2019_1()
        {
            JT809_0x1300_0x1301 jT809_0x1300_0x1301 = new JT809_0x1300_0x1301
            {
                ObjectID = "111",
                InfoContent = "22ha22",
                InfoID = 1234,
                ObjectType = JT809_0x1301_ObjectType.当前连接的下级平台,
                SourceMsgSn=99,
                ResponderTel="12345678901",
                SourceDataType=111,
                Responder="smallchi"
            };
            var hex = JT809_2019_Serializer.Serialize(jT809_0x1300_0x1301).ToHexString();
            Assert.Equal("01736D616C6C636869000000000000000031323334353637383930310000000000000000003131310000000000000000000000000000000000006F0000006300000006323268613232", hex);
        }

        [Fact]
        public void Test_2019_2()
        {
            var bytes = "01736D616C6C636869000000000000000031323334353637383930310000000000000000003131310000000000000000000000000000000000006F0000006300000006323268613232".ToHexBytes();
            JT809_0x1300_0x1301 jT809_0x1300_0x1301 = JT809_2019_Serializer.Deserialize<JT809_0x1300_0x1301>(bytes);
            Assert.Equal("111", jT809_0x1300_0x1301.ObjectID);
            Assert.Equal("22ha22", jT809_0x1300_0x1301.InfoContent);
            Assert.Equal(0u, jT809_0x1300_0x1301.InfoID);
            Assert.Equal(JT809_0x1301_ObjectType.当前连接的下级平台, jT809_0x1300_0x1301.ObjectType);
            Assert.Equal(99u, jT809_0x1300_0x1301.SourceMsgSn);
            Assert.Equal("12345678901", jT809_0x1300_0x1301.ResponderTel);
            Assert.Equal(111, jT809_0x1300_0x1301.SourceDataType);
            Assert.Equal("smallchi", jT809_0x1300_0x1301.Responder);
        }
    }
}
