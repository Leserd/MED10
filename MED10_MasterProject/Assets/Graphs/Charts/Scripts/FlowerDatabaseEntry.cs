public class FlowerDatabaseEntry
{
    public string Species;
    public float SepalWidth;
    public float SepalLength;
    public float PetalWidth;
    public float PetalLength;


    public override string ToString()
    {
        return "Species: " + Species + "\n" +
               "Sepal Lenght: " + SepalLength + "\n" +
               "Sepal Width: " + SepalWidth + "\n" +
               "Petal Lenght: " + PetalLength + "\n" +
               "Petal Width: " + PetalWidth;
    }
}
