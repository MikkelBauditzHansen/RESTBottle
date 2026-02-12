namespace RESTBottle.Bottles
{
    public class GenericRepository<T>
    {
        private List<T> _items = new List<T>();
        public void Add(T item)
        {
            _items.Add(item);
        }
        public List<T> GetAll()
        {
            return _items;
        }
        public T? GetById(Func<T, bool> predicate)
        {
            return _items.FirstOrDefault(predicate);
        }
        public T? Remove(Func<T, bool> predicate)
        {
            var item = _items.FirstOrDefault(predicate);
            if (item != null)
            {
                _items.Remove(item);
            }
            return item;
        }
        public T? Update(Func<T, bool> predicate, T updatedItem)
        {
            var item = _items.FirstOrDefault(predicate);
            if (item != null)
            {
                var index = _items.IndexOf(item);
                _items[index] = updatedItem;
                return updatedItem;
            }
            return default;
        }
    }
}
