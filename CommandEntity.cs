using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEGASuperChatBot
{
    public class CommandEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommandId { get; set; }
        public string CommandTrigger { get; set; }
        public string SourcesNames { get; set; }
        public string CommandAnswer { get; set; }
        public string CommandAuthor { get; set; }
        public string CommandDescription { get; set; }
        public string CommandCreateDate { get; set; }
        public Boolean IsScript { get; set; }
        public String ScriptName { get; set; }
        public string ScriptText { get; set; }
    }
}
