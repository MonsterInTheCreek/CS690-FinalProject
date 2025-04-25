namespace Tasks.Tests;

public class InstantiationTests
{
    public class RemoveAction
    {
        public RemoveAction()
        {
            // delete prior
            if (File.Exists("actions.txt"))
            {
                File.Delete("actions.txt");
            }
        }
    }

    public class RemoveTarget
    {
        public RemoveTarget()
        {
            // delete prior
            if (File.Exists("targets.txt"))
            {
                File.Delete("targets.txt");
            }
        }
    }
    
    public class RemoveSupply
    {
        public RemoveSupply()
        {
            // delete prior
            if (File.Exists("supplies.txt"))
            {
                File.Delete("supplies.txt");
            }
        }
    }
    
    [Fact]
    public void ActionInstantiation()
    {
        // should reinstantiate default dummy data
        ActionManager actionManager = new ActionManager();
        
        List<string> dummyActionData = new List<string> {"clean","dust","wipe","sweep","wash"};

        List<string> actualActionFile = new List<string>();
        var actionsFileData = File.ReadAllLines("actions.txt");
        foreach (string actionName in actionsFileData)
        {
            actualActionFile.Add(actionName);
        }
        
        Assert.Equal(dummyActionData, actualActionFile);
    }    
    
    [Fact]
    public void TargetInstantiation()
    {
        // should reinstantiate default dummy data
        TargetManager targetManager = new TargetManager();
        
        List<string> dummyTargetData = new List<string> {"bathroom","shelves","counter","floor","dishes"};

        List<string> actualTargetFile = new List<string>();
        var targetsFileData = File.ReadAllLines("targets.txt");
        foreach (string targetName in targetsFileData)
        {
            actualTargetFile.Add(targetName);
        }
        
        Assert.Equal(dummyTargetData, actualTargetFile);
    }    
    
    [Fact]
    public void SupplyInstantiation()
    {
        // should reinstantiate default dummy data
        SupplyManager supplyManager = new SupplyManager();
        
        List<string> dummySupplyData = new List<string> {
            "detergent;true;100;false",
            "soap;true;100;false",
            "rags;false;100;false",
            "car wax;true;100;false",
            "broom;false;100;false"
        };

        List<string> actualSupplyFile = new List<string>();
        var suppliesFileData = File.ReadAllLines("supplies.txt");
        foreach (string supplyName in suppliesFileData)
        {
            actualSupplyFile.Add(supplyName);
        }
        
        Assert.Equal(dummySupplyData, actualSupplyFile);
    }    
}



