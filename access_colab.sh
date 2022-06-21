for i in `seq 0 12`
do
  echo "[$i]" ` date '+%y/%m/%d %H:%M:%S'` "connected."
  open <<https://colab.research.google.com/drive/1EjmT2Sj33t7pVrmzRYAe1R67e1j2mctr#scrollTo=bbvmaKNiznHZ>>
  sleep 3600
done