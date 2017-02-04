using EloBuddy;
using EloBuddy.SDK;
using System.Linq;

namespace UBAddons.Champions.Warwick.Modes
{
    class Flee : Warwick
    {
        public static void Execute()
        {
            var rectangle = new Geometry.Polygon.Rectangle(player.Position, Game.CursorPos, 115f);
            var Enemyminions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(m => m.IsValidTarget(Q.Range) && rectangle.IsInside(m)).OrderBy(x => x.Distance(Game.CursorPos));
            var monsters = EntityManager.MinionsAndMonsters.Monsters.Where(m => m.IsValidTarget(Q.Range) && rectangle.IsInside(m)).OrderBy(x => x.Distance(Game.CursorPos));
            var champs = EntityManager.Heroes.Enemies.Where(c => c.IsValidTarget(Q.Range) && rectangle.IsInside(c)).OrderBy(x => x.Distance(Game.CursorPos));
            if (Enemyminions.Any() && MenuValue.Flee.QMinion)
            {
                Q.Cast(Enemyminions.First());
            }
            if (player.HealthPercent > MenuValue.Flee.HP)
            {
                if (monsters.Any() && MenuValue.Flee.QMonster)
                {
                    Q.Cast(monsters.First());
                }
                if (champs.Any() && MenuValue.Flee.QChamp)
                {
                    Q.Cast(champs.First());
                }
            }
        }
    }
}
