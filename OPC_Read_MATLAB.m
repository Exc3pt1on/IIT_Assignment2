clear, clc

%Connect to server
da = opcda('localhost', 'Matrikon.OPC.Simulation.1');
connect(da);

%Create group
grp = addgroup(da, 'Group1');

%Add tags
ItemList = {'Bucket Brigade.Real4'};
item = additem(grp, ItemList);

%Create loop
while true
    %Retrieve data
    data = read(grp);
    opcdata = data.Value;
    fahrenheit = (opcdata*(9/5))+32;
    fprintf("OPC_Read: Value: %dC %dF\n",opcdata, fahrenheit);
    pause(2)
end


