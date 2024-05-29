using System;
using System.Text;


public static class RandomExtensions {
    private static readonly char[] chars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    public static string nextString(this Random self,int length=10){
        StringBuilder result=new StringBuilder(length);
        for(int i=0;i<length;i++){
            result.Append(chars[self.Next(chars.Length)]);
        }
        return result.ToString();
    }
}
