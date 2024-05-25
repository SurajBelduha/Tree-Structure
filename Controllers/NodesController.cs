using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using Task.ViewModel;

namespace Task.Controllers
{
    public class NodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NodesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var activeNodes = _context.Nodes.FromSqlRaw("SELECT * FROM Nodes WHERE IsActive = 1").ToList();
            return View(activeNodes);
        }
        // GET: Nodes/Create
        public IActionResult Create()
        {
            ViewData["ParentNodeId"] = new SelectList(_context.Nodes.Where(x => x.IsActive == true), "NodeId", "NodeName");
            return View();
        }

        // POST: Nodes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NodeModel model)
        {
            if (ModelState.IsValid)
            {
                Node node = new Node()
                {
                    NodeId = model.NodeId,
                    ParentNodeId = model.ParentNodeId,
                    NodeName = model.NodeName,
                    IsActive = model.IsActive,
                    StartDate = model.StartDate,
                };
                _context.Add(node);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Nodes");
            }
            ViewData["ParentNodeId"] = new SelectList(_context.Nodes, "NodeId", "NodeName", model.ParentNodeId);
            return View(model);
        }

        public IActionResult Tree()
        {
            return View();
        }

        [HttpPost]
        public JsonResult TreeData()
        {
            var nodes = _context.Nodes.Where(x => x.IsActive == true).ToList();
            var treeNodes = BuildTree(nodes);
            return Json(treeNodes);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var node = await _context.Nodes.FindAsync(id);
            if (node == null)
            {
                return RedirectToAction("Index", "Nodes");
            }
            NodeModel nodeModel = new NodeModel()
            {
                NodeId = node.NodeId,
                NodeName = node.NodeName,
                ParentNodeId = node.ParentNodeId,
                IsActive = node.IsActive,
                StartDate = node.StartDate,
            };
            ViewData["ParentNodeId"] = new SelectList(_context.Nodes.Where(x => x.IsActive == true), "NodeId", "NodeName");
            return View(nodeModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(NodeModel nodeModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ParentNodeId"] = new SelectList(_context.Nodes.Where(x => x.IsActive == true), "NodeId", "NodeName");
                return View(nodeModel);
            }
            var node = await _context.Nodes.FindAsync(nodeModel.NodeId);
            if (node == null)
            {
                return NotFound();
            }
            node.NodeName = nodeModel.NodeName;
            node.ParentNodeId = nodeModel.ParentNodeId;
            node.IsActive = nodeModel.IsActive;
            node.StartDate = nodeModel.StartDate;
            _context.Nodes.Update(node);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var node = await _context.Nodes.FindAsync(id);
            if (node == null)
            {
                return NotFound();
            }
            node.IsActive=false;
            _context.Nodes.Update(node);
            //_context.Nodes.Remove(node);
            await _context.SaveChangesAsync();
            return Ok();
        }


        #region Private method
        private List<TreeNode> BuildTree(List<Node> nodesList, int? parentId = 0)
        {
            var returnodes = new List<TreeNode>();
            return nodesList
                .Where(n => n.ParentNodeId == parentId)
                .Select(n => new TreeNode
                {
                    id = n.NodeId,
                    text = n.NodeName,
                    children = BuildTree(nodesList, n.NodeId)
                })
                .ToList();
        }
        #endregion
    }

}
