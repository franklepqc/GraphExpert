namespace GraphExpert.Animations
{
    public class Deplacement : IDeplacement
    {
        public Deplacement(byte agentId, byte portId)
        {
            AgentId = agentId;
            PortId = portId;
        }

        public byte AgentId { get; }

        public byte PortId { get; }
    }
}
