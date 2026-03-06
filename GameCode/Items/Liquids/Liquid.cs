namespace ConsoleApp1.GameCode.Items;

public class Liquid
{
    private
        int amount=0;
    
    public
        Liquid(int amount){
            this.amount = amount;
        }
    
    void addAmount(int amount) {
        this.amount += amount;
    }
    
    //Vrati momentalny pocet
    int getAmount() {
        return amount;
    }
    
    //Chceme odobrat nej
    int getAmount(int amount) {
        this.amount -= Math.Min(amount, this.amount);
        return Math.Min(amount, this.amount);
    }

}