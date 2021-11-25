

public class ObjectPoolContainer<T>
{
    private T item;

    public bool Used{get; set;}

    public void UseThis()
    {
        Used = true;
    }

    public T Item
    {
        get
        {
            return item;
        }

        set
        {
            item = value;
        }
    }

    public void Release()
    {
        Used = false;
    }
}
