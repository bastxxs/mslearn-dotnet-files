using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir); 


var salesFiles = FindFiles(storesDirectory);
 
var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

IEnumerable<String> FindFiles(String folderName){

List<String> salesFiles = new List<String>();

var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

foreach(var file in foundFiles){

var extension = Path.GetExtension(file);

if(extension == ".json"){

salesFiles.Add(file);

}

}

return salesFiles;
}


double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    foreach(var file in salesFiles){

        string salesJson = File.ReadAllText(file);

        
        salesData? data = JsonConvert.DeserializeObject<salesData?>(salesJson);


         salesTotal += data?.total ?? 0;


    }
    return salesTotal;
}


record salesData (double total);

class SalesTotal{

public double total { get; set; }

}
