using UnityEngine;
using System.Collections;

public class CircularBuffer {

	public CircularBuffer(int count){
		_max = count;
		_top = 0;
		_arr = new string[_max];
		_length = 0;
		_peeker = 0;
	}

	private int _top;
	private int _length;
	private int _peeker; //used to traverse the stack multiple times
	private int _max;
	private string[] _arr;

	public void Push(string comm){
		if (_top < _max - 1) {
			_arr [_top] = comm;
			_top++;
		} else {
			_top = 0;
			_arr [_max - 1] = comm;
		}
		if (_length < _max) {
			_length++;
		}
		_peeker = _top;
	}

	public string Peek(string dir){
		if (dir == "up") {
			_peeker--;
		} else if (dir == "down") {
			_peeker++;
		}
		if (_peeker <= -1 && _length != 0) {
			_peeker = _length - 1;
		}
		if (_peeker >= _max) {
			_peeker = 0;
		}
        Debug.Log(_peeker);
		return _arr [_peeker];
	}
    public string[] GetLastFive()
    {
        string[] temp = {" ", " ", " ", " ", " "};
        if (_length >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                temp[i] = Peek("up");
            }
            _peeker += 5;
        }
        else
        {
            for (int i = 0; i < _length; i++)
            {
                    temp[i] = Peek("up");
            }
            _peeker += _length;
        }
        if (_peeker >= _length)
            _peeker -= _length;
        return temp;
    }
}
