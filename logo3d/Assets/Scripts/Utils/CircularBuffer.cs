using UnityEngine;
using System.Collections;

public class CircularBuffer : MonoBehaviour {

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
		_peeker = _top - 1;

	}
	public string Peek(){
		if (_peeker <= -1) {
			_peeker = _length - 1;
		}
		var temp = _peeker;
		_peeker--;
		return _arr [temp];
	}
}
