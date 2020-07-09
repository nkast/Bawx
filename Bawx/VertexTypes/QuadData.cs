﻿using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bawx.VertexTypes
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    public struct QuadData : IVertexType
    {
        public byte X;
        public byte Y;
        public byte Z;
        public byte Index;

        public byte Normal;

        public static readonly VertexDeclaration VertexDeclaration;

        public QuadData(byte x, byte y, byte z, byte index, byte normal)
        {
            X = x;
            Y = y;
            Z = z;
            Index = index;
            Normal = normal;
        }

        public QuadData(byte[] bytes, byte normal)
        {
            X = bytes[0];
            Y = bytes[1];
            Z = bytes[2];
            Index = bytes[3];
            Normal = normal;
        }

        public Vector3 Position
        {
            get { return new Vector3(X, Y, Z); }
            set
            {
                X = (byte) value.X;
                Y = (byte) value.Y;
                Z = (byte) value.Z;
            }
        }


        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;

        public override string ToString()
        {
            return $"Position: ({X}, {Y}, {Z}), Index: {Index}, Direction: {Normal}";
        }

        public bool Equals(QuadData other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && Index == other.Index && Normal == other.Normal;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is QuadData && Equals((QuadData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = hashCode ^ Y.GetHashCode();
                hashCode = hashCode ^ Z.GetHashCode();
                hashCode = hashCode ^ Index.GetHashCode();
                hashCode = hashCode ^ Normal.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(QuadData a, QuadData b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(QuadData a, QuadData b)
        {
            return !(a == b);
        }

        static QuadData()
        {
            var elements = new [] 
            { 
                new VertexElement(0, VertexElementFormat.Byte4, VertexElementUsage.Position, 0),
                // TODO extra lighting data goes here, only the first byte is taken by normal
                new VertexElement(4, VertexElementFormat.Byte4, VertexElementUsage.Normal, 0), 
            };
            VertexDeclaration = new VertexDeclaration(elements);
        }
    }
}
