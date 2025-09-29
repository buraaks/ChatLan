using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanChatApp.Models
{
    // Sohbet mesajlarını temsil eden veri modeli
    public class ChatMessage
    {
        public string Type { get; set; } = "chat";   // chat, system, file, file-chunk
        public string Sender { get; set; } = "";     // Gönderenin adı
        public string Message { get; set; } = "";    // Mesaj veya dosya adı
        public string Timestamp { get; set; } = "";  // HH:mm:ss
        public string? FileContentBase64 { get; set; } // Dosya içeriği (tam dosya veya parça)

        // Parçalı gönderim için meta bilgiler
        public int? ChunkIndex { get; set; }         // Kaçıncı parça (0-based)
        public int? TotalChunks { get; set; }        // Toplam parça sayısı
    }

    // ListBox içinde dosya mesajını temsil eden yardımcı sınıf (çift tıkla indirme için)
    public class ChatListItem
    {
        public string Text { get; set; } = "";
        public string? FileName { get; set; }
        public string? FileContentBase64 { get; set; } // Tam dosya içerik (parçalar birleştirildikten sonra)

        public override string ToString() => Text;
    }


}
