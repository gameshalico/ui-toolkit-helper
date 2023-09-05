namespace UIToolkitHelper.FillBars
{
    public interface IFillable
    {
        public float fillAmount { get; set; }

        public FillDirection fillDirection { get; set; }
    }
}