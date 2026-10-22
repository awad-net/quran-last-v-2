namespace Quran
{
    public interface Inoti
    {

        void Remindfajr(int id, int hour, int minute, string title, string message, int day, int year);
        void Remindduhr(int id, int hour, int minute, string title, string message, int day, int year);
        void Remindasr(int id, int hour, int minute, string title, string message, int day, int year);

        void Remindmaghrb(int id, int hour, int minute, string title, string message, int day, int year);
        void Remindisha(int id, int hour, int minute, string title, string message, int day, int year);

    }


    public interface InotiiosAndandorid
    {

        void Remindfajr(string id, string title, string body, int hour, int minute, int day, int mounth, int year);
        void Remindduhr(string id, string title, string body, int hour, int minute, int day, int mounth, int year);
        void Remindasr(string id, string title, string body, int hour, int minute, int day, int mounth, int year);
        void Remindmaghreb(string id, string title, string body, int hour, int minute, int day, int mounth, int year);
        void Remindisha(string id, string title, string body, int hour, int minute, int day, int mounth, int year);
        void repeting(string id, string title, string body);
        void cancellallnotification();



    }




}